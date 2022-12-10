using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadFlow.Auth.Domain.RefreshToken;
using RoadFlow.Auth.Domain.SignIn;
using RoadFlow.Auth.Domain.SIgnUp;
using RoadFlow.Common.Extensions;

namespace RoadFlow.Auth.API.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = ArgumentNullValidator.ThrowIfNullOrReturn(mediator);
    }
    
    [HttpPost]
    [Route("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
    
    [HttpPost]
    [Route("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPost]
    [Route("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}