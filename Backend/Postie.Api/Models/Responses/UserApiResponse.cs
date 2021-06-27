using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Models.Responses
{
    public class UserApiResponse
    {
        public string Username { get; set; }
        
        public IEnumerable<PostApiResponse> RecentPosts { get; set; }
        
        public IEnumerable<CommentApiResponse> RecentComments { get; set; }
    }
}
