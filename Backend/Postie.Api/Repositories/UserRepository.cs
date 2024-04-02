using System.Linq;
using Postie.Api.Data;
using Postie.Api.Models.Db;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserByUsername(string username)
        {
            return (User)_dbContext.Users.FirstOrDefault(x => x.UserName == username);
        }
    }
}
