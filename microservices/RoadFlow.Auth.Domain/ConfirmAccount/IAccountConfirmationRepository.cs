namespace RoadFlow.Auth.Domain.ConfirmAccount;

public interface IAccountConfirmationRepository
{
    Task InsertOrUpdateCode(AccountConfirmation accountConfirmation, CancellationToken cancellationToken = default);
    Task<AccountConfirmation> Get(string email, CancellationToken cancellationToken = default);
}