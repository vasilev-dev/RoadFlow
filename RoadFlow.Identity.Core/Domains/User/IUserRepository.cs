namespace RoadFlow.Identity.Core.Domains.User;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<bool> ExistsWithEmail(string email);
    Task<bool> ExistsWithUsername(string username);
    Task<User> GetByEmail(string email);
}