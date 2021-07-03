using System.Collections.Generic;

namespace Postie.Api.Models.Responses
{
    public class UserApiResponse
    {
        public string Username { get; set; }

        public IEnumerable<PostApiResponse> RecentPosts { get; set; }

        public IEnumerable<CommentApiResponse> RecentComments { get; set; }
    }
}
