using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoadFlow.Common.Exceptions;
using RoadFlow.Seedwork.ApplicationUser;

namespace RoadFlow.Auth.Domain.SIgnUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SignUpCommandHandler(
        UserManager<ApplicationUser> userManager)
    {
        ArgumentNullException.ThrowIfNull(userManager);

        _userManager = userManager;
    }

    public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (_userManager.Users.Any(u => u.Email == request.Email))
            throw new ClientException("1");

        if (_userManager.Users.Any(u => u.UserName == request.Username))
            throw new ClientException("2");

        var userId = Guid.NewGuid().ToString();

        var user = new ApplicationUser
        {
            Id = userId,
            UserName = request.Username,
            EmailConfirmed = false,
            Email = request.Email,
            Roles = new List<string> { ApplicationRoles.Default }
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new ServerException("Cannot create user"); // todo log detail

        // await SaveUserRole(user, ApplicationRoles.Default);
        await SaveUserClaims(user, ApplicationRoles.Default);

        return Unit.Value;
    }
    
    private async Task SaveUserClaims(ApplicationUser user, string role)
    {
        var claims = new List<Claim>
        {
            new("username", user.UserName),
            new("email", user.Email),
            new("role", role)
        };
        
        await _userManager.AddClaimsAsync(user, claims);
    }
}