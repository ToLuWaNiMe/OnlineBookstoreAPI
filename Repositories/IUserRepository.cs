using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<int> AddUser(User user);
    }
}
