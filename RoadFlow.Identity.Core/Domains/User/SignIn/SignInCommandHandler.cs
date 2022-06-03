using MediatR;
using RoadFlow.Common.Errors;
using RoadFlow.Identity.Core.Domains.Token;
using RoadFlow.Identity.Core.Domains.User.Common;
using RoadFlow.Identity.Core.Domains.User.Common.Password;

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
        var (email, password) = request;
        
        var user = await _userRepository.GetByEmail(email);

        if (user == null)
            throw new ClientException(ClientErrorCode.UserDoesNotExist);

        if (!_passwordService.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            throw new ClientException(ClientErrorCode.WrongPassword);

        var (accessToken, expirationAccessTokenTime) = 
            _tokenService.GenerateAccessToken(user.Id, user.Email, user.Username, user.Role);
        var (refreshToken, expirationRefreshTokenTime) = _tokenService.GenerateRefreshToken();
        
        // todo не обновляется refresh token в бд

        return new TokenResponse(accessToken, expirationAccessTokenTime, 
            refreshToken, expirationRefreshTokenTime);
    }
}