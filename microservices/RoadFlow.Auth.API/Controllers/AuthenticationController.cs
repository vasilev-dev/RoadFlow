﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadFlow.Auth.Domain.RefreshToken;
using RoadFlow.Auth.Domain.SignIn;
using RoadFlow.Auth.Domain.SIgnUp;

namespace RoadFlow.Auth.API.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
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
    [Authorize]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}