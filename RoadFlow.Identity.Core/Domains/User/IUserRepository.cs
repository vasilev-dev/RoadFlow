namespace RoadFlow.Identity.Core.Domains.User;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<bool> ExistsWithEmail(string email);
    Task<User> GetByEmail(string email);
}