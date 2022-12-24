using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using RoadFlow.Common.Exceptions;
using RoadFlow.Common.Extensions;
using RoadFlow.Seedwork.ApplicationUser;

namespace RoadFlow.Auth.IdentityServer;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

    public ProfileService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
    {
        _userManager = ArgumentNullValidator.ThrowIfNullOrReturn(userManager);
        _claimsFactory = ArgumentNullValidator.ThrowIfNullOrReturn(claimsFactory);
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        if (user == null)
            throw new ServerException("Cannot add claims to access token. User not found");

        var principal = await _claimsFactory.CreateAsync(user);
        var claims = principal.Claims.ToList();
        
        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user is {EmailConfirmed: true};
    }
}