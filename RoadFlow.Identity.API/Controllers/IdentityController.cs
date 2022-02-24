using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadFlow.Identity.Core.Domains.User.SignUp;

namespace RoadFlow.Identity.API.Controllers;

[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpPost(Route.SignUp)]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand signUpCommand)
    {
        var tokenResponse = await _mediator.Send(signUpCommand);

        return Ok(tokenResponse);
    }
    
    [AllowAnonymous]
    [Route("account/google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
 
    [AllowAnonymous]
    [Route("account/google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
 
        var claims = result.Principal.Identities
            .FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

        return Ok(claims);
    }
    
    // [HttpPost(Route.SignIn)]
    // public IActionResult SignIn([FromBody] SignInCommand signInCommand)
    // {
    //     var isRoot = signInCommand.Email == "root" && signInCommand.Password == "Password1!";
    //
    //     if (!isRoot) return Unauthorized();
    //     
    //     var authClaims = new List<Claim>
    //     {
    //         new(ClaimTypes.Email, signInCommand.Email),
    //         new(ClaimTypes.Name, "todo"),
    //         new(ClaimTypes.Role, "Root")
    //     };
    //     
    //     var (accessToken, expirationAccessTokenTime) = GenerateAccessToken(authClaims);
    //     var (refreshToken, expirationRefreshTokenTime) = GenerateRefreshToken();
    //     
    //     // todo save refresh token in db
    //
    //     return Ok(new
    //     {
    //         accessToken,
    //         expirationAccessTokenTime,
    //         refreshToken,
    //         expirationRefreshTokenTime
    //     });
    // }
}