using MediatR;
using Microsoft.AspNetCore.Identity;
using RoadFlow.Common.Exceptions;
using RoadFlow.Common.Extensions;
using RoadFlow.Seedwork.ApplicationUser;

namespace RoadFlow.Auth.Domain.ConfirmAccount.ValidateConfirmationCode;

public class ValidateConfirmationCodeCommandHandler : IRequestHandler<ValidateConfirmationCodeCommand, Unit>
{
    private readonly IAccountConfirmationRepository _accountConfirmationRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ValidateConfirmationCodeCommandHandler(
        IAccountConfirmationRepository accountConfirmationRepository,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = ArgumentNullValidator.ThrowIfNullOrReturn(userManager);
        _accountConfirmationRepository = ArgumentNullValidator.ThrowIfNullOrReturn(accountConfirmationRepository);
    }
    
    public async Task<Unit> Handle(ValidateConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        var accountConfirmation = await _accountConfirmationRepository.Get(request.Email, cancellationToken);

        if (accountConfirmation is null)
            throw new ClientException("", 
                $"Confirmation code for user with email = {request.Email} not found");

        if (DateTime.UtcNow > accountConfirmation.ExpiryDate)
            throw new ClientException(ErrorCode.ConfirmationCodeIsExpired, "Confirmation code is expired");

        if (accountConfirmation.Code != request.Code)
            throw new ClientException(ErrorCode.WrongConfirmationCode, "Wrong confirmation code");

        var user = _userManager.Users.FirstOrDefault(u => u.Email == request.Email);
        
        if (user is null)
            throw new ClientException(ErrorCode.UserNotFound, $"User with email = {request.Email} not found");
        
        if (user.EmailConfirmed)
            throw new ClientException(ErrorCode.EmailAlreadyConfirmed, 
                $"User with email = {request.Email} already confirmed");

        user.EmailConfirmed = true;

        await _userManager.UpdateAsync(user);

        return Unit.Value;
    }
}