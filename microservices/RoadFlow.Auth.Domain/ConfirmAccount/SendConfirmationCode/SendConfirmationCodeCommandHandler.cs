using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RoadFlow.Common.Exceptions;
using RoadFlow.Common.Extensions;
using RoadFlow.Seedwork.ApplicationUser;
using RoadFlow.Seedwork.Events;

namespace RoadFlow.Auth.Domain.ConfirmAccount.SendConfirmationCode;

public class SendConfirmationCodeCommandHandler : IRequestHandler<SendConfirmationCodeCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IAccountConfirmationRepository _accountConfirmationRepository;

    public SendConfirmationCodeCommandHandler(
        UserManager<ApplicationUser> userManager,
        IPublishEndpoint publishEndpoint,
        IAccountConfirmationRepository accountConfirmationRepository)
    {
        _accountConfirmationRepository = accountConfirmationRepository;
        _userManager = ArgumentNullValidator.ThrowIfNullOrReturn(userManager);
        _publishEndpoint = ArgumentNullValidator.ThrowIfNullOrReturn(publishEndpoint);
        _accountConfirmationRepository = ArgumentNullValidator.ThrowIfNullOrReturn(accountConfirmationRepository);
    }

    public async Task<Unit> Handle(SendConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Email == request.Email);
        
        if (user is null)
            throw new ClientException(ErrorCode.UserNotFound, $"User with email = {request.Email} not found");

        if (user.EmailConfirmed)
            throw new ClientException(ErrorCode.EmailAlreadyConfirmed, 
                $"User with email = {request.Email} already confirmed");

        var activationCode = new Random().Next(0, 999999).ToString("000000");

        var accountConfirmation = new AccountConfirmation
        {
            Email = request.Email,
            Code = activationCode,
            ExpiryDate = DateTime.UtcNow.AddHours(1)
        };

        await _accountConfirmationRepository.InsertOrUpdateCode(accountConfirmation, cancellationToken);

        var sendEmailEvent = new SendEmailEvent(
            ToEmail: request.Email, 
            Subject:"Email confirmation", 
            ToName: user.UserName!,
            PlainTextContent: $"Activation code: {activationCode}"
        );
        
        await _publishEndpoint.Publish(sendEmailEvent, cancellationToken);
        
        return Unit.Value;
    }
}