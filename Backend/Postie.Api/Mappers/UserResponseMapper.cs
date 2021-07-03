using System.Collections.Generic;
using System.Linq;
using Postie.Api.Models.Db;
using Postie.Api.Models.Responses;

namespace Postie.Api.Mappers
{
    public class UserResponseMapper
    {
        public IEnumerable<UserApiResponse> MapDbToResponseList(IEnumerable<User> users)
        {
            return users.Select(MapDbToResponse).ToList();
        }

        public UserApiResponse MapDbToResponse(User user)
        {
            return new UserApiResponse
            {
                Username = user.Username
            };
        }
    }
}
