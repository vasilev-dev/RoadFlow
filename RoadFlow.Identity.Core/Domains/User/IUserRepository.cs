namespace RoadFlow.Identity.Core.Domains.User;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<bool> ExistsWithUsername(string username);
    Task<bool> ExistsWithEmail(string email);
}