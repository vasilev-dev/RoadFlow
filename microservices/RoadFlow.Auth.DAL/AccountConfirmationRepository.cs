using MongoDB.Driver;
using RoadFlow.Auth.Domain.ConfirmAccount;

namespace RoadFlow.Auth.DAL;

public class AccountConfirmationRepository : IAccountConfirmationRepository
{
    private readonly IMongoCollection<AccountConfirmation> _collection;

    public AccountConfirmationRepository(IMongoDatabase database)
    {
        ArgumentNullException.ThrowIfNull(database);
        _collection = database.GetCollection<AccountConfirmation>("AccountConfirmation");
    }
    
    public async Task InsertOrUpdateCode(AccountConfirmation accountConfirmation, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(x => x.Email == accountConfirmation.Email, 
            accountConfirmation, new UpdateOptions {IsUpsert = true}, cancellationToken: cancellationToken);
    }

    public async Task<AccountConfirmation> Get(string email, CancellationToken cancellationToken)
    {
        var filter = Builders<AccountConfirmation>.Filter.Where(x => x.Email == email);
        var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}