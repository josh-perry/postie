using Postie.Api.Models.Db;

namespace Postie.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
    }
}
