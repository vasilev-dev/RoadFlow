using MediatR;
using RoadFlow.Common.Domains.User;
using RoadFlow.Common.Errors;
using RoadFlow.Identity.Core.Domains.Password;
using RoadFlow.Identity.Core.Domains.Token;
using RoadFlow.Identity.Core.Domains.User.Common;

namespace RoadFlow.Identity.Core.Domains.User.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public SignUpCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }
    
    public async Task<TokenResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var (username, email, password) = request;
        
        if (await _userRepository.ExistsWithEmail(email))
            throw new ClientException(ClientErrorCode.UserWithEmailAlreadyExistsError);

        const string userRole = Role.User;

        var (passwordHash, passwordSalt) = _passwordService.GeneratePasswordHashAndSalt(password);
        var (accessToken, expirationAccessTokenTime) = _tokenService.GenerateAccessToken(email, username, userRole);
        var (refreshToken, expirationRefreshTokenTime) = _tokenService.GenerateRefreshToken();

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = userRole,
            RefreshToken = refreshToken,
            ExpirationRefreshTokenTime = expirationRefreshTokenTime
        };

        await _userRepository.CreateUser(user);

        return new TokenResponse(accessToken, expirationAccessTokenTime, refreshToken, expirationRefreshTokenTime);
    }
}