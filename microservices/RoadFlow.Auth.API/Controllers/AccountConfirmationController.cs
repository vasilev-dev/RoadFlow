using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadFlow.Auth.Domain.ConfirmAccount.SendConfirmationCode;
using RoadFlow.Auth.Domain.ConfirmAccount.ValidateConfirmationCode;
using RoadFlow.Common.Extensions;

namespace RoadFlow.Auth.API.Controllers;

[ApiController]
public class AccountConfirmationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountConfirmationController(IMediator mediator)
    {
        _mediator = ArgumentNullValidator.ThrowIfNullOrReturn(mediator);
    }
    
    [HttpPut]
    [Route("account-confirmation")]
    [AllowAnonymous]
    public async Task<IActionResult> SendConfirmationCode([FromBody] SendConfirmationCodeCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
    
    [HttpPost]
    [Route("account-confirmation/validate")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidateConfirmationCode([FromBody] ValidateConfirmationCodeCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}