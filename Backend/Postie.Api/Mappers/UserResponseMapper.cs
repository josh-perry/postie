using System.Collections.Generic;
using Postie.Api.Models.Db;
using Postie.Api.Models.Responses;

namespace Postie.Api.Mappers
{
    public class UserResponseMapper
    {
        public IEnumerable<UserApiResponse> MapDbToResponseList(IEnumerable<User> users)
        {
            var result = new List<UserApiResponse>();

            foreach (var user in users)
                result.Add(MapDbToResponse(user));

            return result;
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
