using MongoDB.Driver;
using RoadFlow.Data;
using RoadFlow.Identity.Core.Domains.User;

namespace RoadFlow.Identity.Data.Domains.User;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<Core.Domains.User.User> _userCollection;

    public UserRepository(IMongoContext mongoContext)
    {
        _userCollection = mongoContext.Database.GetCollection<Core.Domains.User.User>("Users");
    }

    public async Task CreateUser(Core.Domains.User.User user)
    {
        await _userCollection.InsertOneAsync(user);
    }
    
    public async Task<bool> ExistsWithEmail(string email)
    {
        return await _userCollection.Find(user => user.Email == email).AnyAsync();
    }

    public async Task<Core.Domains.User.User> GetByEmail(string email)
    {
        return await _userCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
    }
}