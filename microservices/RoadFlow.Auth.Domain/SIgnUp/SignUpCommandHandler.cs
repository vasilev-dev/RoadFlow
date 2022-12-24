using System.Collections.Immutable;
using System.Security.Claims;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoadFlow.Common.Exceptions;
using RoadFlow.Common.Extensions;
using RoadFlow.Seedwork.ApplicationUser;
using RoadFlow.Seedwork.Events;
using Serilog;

namespace RoadFlow.Auth.Domain.SIgnUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger _logger;

    public SignUpCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger logger)
    {
        _userManager = ArgumentNullValidator.ThrowIfNullOrReturn(userManager);
        _logger = ArgumentNullValidator.ThrowIfNullOrReturn(logger);
    }

    public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (_userManager.Users.Any(u => u.Email == request.Email))
            throw new ClientException(ErrorCode.EmailAlreadyIsUsed, 
                $"Email {request.Email} already is used");
        
        if (_userManager.Users.Any(u => u.UserName == request.Username))
            throw new ClientException(ErrorCode.UsernameAlreadyIsUsed,
                $"Username {request.Username} already is used");

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
        {
            _logger.Error("Cannot create user: {@Errors}", result.Errors.ToImmutableList());
            throw new ServerException("Cannot create user");
        }

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