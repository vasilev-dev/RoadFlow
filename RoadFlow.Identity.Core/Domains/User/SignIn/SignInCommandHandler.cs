using MediatR;
using RoadFlow.Common.Errors;
using RoadFlow.Identity.Core.Domains.Password;
using RoadFlow.Identity.Core.Domains.Token;
using RoadFlow.Identity.Core.Domains.User.Common;

namespace RoadFlow.Identity.Core.Domains.User.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public SignInCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }
    
    public async Task<TokenResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user == null)
            throw new ClientException(ClientErrorCode.UserDoesNotExist);

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new ClientException(ClientErrorCode.WrongPassword);

        var (accessToken, expirationAccessTokenTime) = _tokenService.GenerateAccessToken(user.Email, user.Username, user.Role);
        var (refreshToken, expirationRefreshTokenTime) = _tokenService.GenerateRefreshToken();

        return new TokenResponse(accessToken, expirationAccessTokenTime, refreshToken, expirationRefreshTokenTime);
    }
}