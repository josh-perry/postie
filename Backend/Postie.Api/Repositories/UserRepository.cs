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

        public User GetUserByName(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == username);
        }

        public User GetUserByAuthId(string authId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.AuthId == authId);
        }
    }
}
