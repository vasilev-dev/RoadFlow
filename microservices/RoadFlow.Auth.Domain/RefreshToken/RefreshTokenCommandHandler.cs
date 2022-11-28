using IdentityModel.Client;
using MediatR;
using RoadFlow.Auth.Common.Configurations;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.Exceptions;
using TokenResponse = RoadFlow.Auth.Domain.Common.Responses.TokenResponse;

namespace RoadFlow.Auth.Domain.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    private readonly SharedConfiguration _sharedConfiguration;
    private readonly IdentityServerConfiguration _identityServerConfiguration;

    public RefreshTokenCommandHandler(
        SharedConfiguration sharedConfiguration,
        IdentityServerConfiguration identityServerConfiguration)
    {
        ArgumentNullException.ThrowIfNull(sharedConfiguration);
        ArgumentNullException.ThrowIfNull(identityServerConfiguration);
        
        _sharedConfiguration = sharedConfiguration;
        _identityServerConfiguration = identityServerConfiguration;
    }
    
    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync(
            _sharedConfiguration.IdentitySettings.Authority, cancellationToken);

        if (disco == null || disco.IsError)
            throw new ServerException($"Cannot connect to Identity Server");
        
        var tokenResponse = await client.RequestTokenAsync(new TokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = _identityServerConfiguration.ClientId,
            GrantType = "refresh_token",
            Parameters = 
            {
                {"refresh_token", $"{request.RefreshToken}"}
            }
        }, cancellationToken);

        if (tokenResponse.IsError)
            throw new ServerException("Cannot refresh token"); // todo log

        return new TokenResponse(
            tokenResponse.AccessToken,
            tokenResponse.RefreshToken,
            DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
            DateTime.Now.AddDays(30)
        );
    }
}