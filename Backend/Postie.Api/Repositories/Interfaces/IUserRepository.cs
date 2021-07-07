using Postie.Api.Models.Db;

namespace Postie.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByName(string username);

        User GetUserByAuthId(string authId);
    }

}
