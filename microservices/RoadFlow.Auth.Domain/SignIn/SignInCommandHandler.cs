using IdentityModel.Client;
using MediatR;
using RoadFlow.Auth.Common.Configurations;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.Exceptions;
using RoadFlow.Common.Extensions;
using Serilog;
using TokenResponse = RoadFlow.Auth.Domain.Common.Responses.TokenResponse;

namespace RoadFlow.Auth.Domain.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, TokenResponse>
{
    private readonly SharedConfiguration _sharedConfiguration;
    private readonly IdentityServerConfiguration _identityServerConfiguration;
    private readonly ILogger _logger;

    public SignInCommandHandler(
        SharedConfiguration sharedConfiguration,
        IdentityServerConfiguration identityServerConfiguration,
        ILogger logger)
    {
        _sharedConfiguration = ArgumentNullValidator.ThrowIfNullOrReturn(sharedConfiguration);
        _identityServerConfiguration = ArgumentNullValidator.ThrowIfNullOrReturn(identityServerConfiguration);
        _logger = ArgumentNullValidator.ThrowIfNullOrReturn(logger);
    }
    
    public async Task<TokenResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
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
            GrantType = "password",
            Parameters =
            {
                {"username", request.Username},
                {"password", request.Password},
            }
        }, cancellationToken);

        if (tokenResponse.IsError)
        {
            _logger.Warning("Cannot create token for username {Username}: {@Response}", request.Username, tokenResponse);
            throw new ClientException(ClientErrorCode.WrongEmailOrPassword, $"Cannot create token for username {request.Username}");
        }
        
        return new TokenResponse(
            tokenResponse.AccessToken,
            tokenResponse.RefreshToken,
            DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
            DateTime.Now.AddDays(30)
        );
    }
}