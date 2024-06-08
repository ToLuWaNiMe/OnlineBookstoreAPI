using Dapper;
using OnlineBookstore.Models;

namespace OnlineBookstore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var query = "SELECT * FROM Users WHERE Username = @Username";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
            }
        }

        public async Task<int> AddUser(User user)
        {
            var query = "INSERT INTO Users (Username, PasswordHash, PasswordSalt) VALUES (@Username, @PasswordHash, @PasswordSalt)";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, user);
            }
        }
    }

}
