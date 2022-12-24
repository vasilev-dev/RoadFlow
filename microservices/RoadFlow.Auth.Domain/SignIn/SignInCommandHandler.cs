using IdentityModel.Client;
using MediatR;
using RoadFlow.Auth.Common.Configurations;
using RoadFlow.Auth.Common.Extensions;
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
            _sharedConfiguration.IdentityConfiguration.Authority, cancellationToken);

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

        ValidateAndThrowTokenResponse(tokenResponse, request.Username);
        
        return new TokenResponse(
            tokenResponse.AccessToken,
            tokenResponse.RefreshToken,
            DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
            DateTime.Now.AddDays(30)
        );
    }
    
    private void ValidateAndThrowTokenResponse(IdentityModel.Client.TokenResponse? tokenResponse, string username)
    {
        if (tokenResponse == null)
            throw new ServerException("Couldn't get token response");
        
        if (tokenResponse.WrongEmailOrPassword())
        {
            _logger.Information("Cannot create token for username {Username}: Wrong username or password", username);
            throw new UnauthorizedException(ErrorCode.WrongUsernameOrPassword, "Username or password is wrong");
        }
        
        if (tokenResponse.UserNotActivated())
        {
            _logger.Warning("Cannot create token for username {Username}: User not activated", username);
            throw new UnauthorizedException(ErrorCode.UserNotActivated, "User not activated");
        }
        
        if (tokenResponse.IsError)
        {
            _logger.Error("Unhandled token response error: {@Response}", tokenResponse);
            throw new ServerException("Unhandled token response error");
        }
    }
}