using System.Collections.Generic;
using System.Linq;
using Postie.Api.Models.Db;

namespace Postie.Api.Mappers
{
    public class PostResponseMapper
    {
        public IEnumerable<PostApiResponse> MapDbToResponseList(IEnumerable<Post> posts)
        {
            return posts.Select(MapDbToResponse).ToList();
        }

        public PostApiResponse MapDbToResponse(Post post)
        {
            return new PostApiResponse
            {
                ID = post.ID,
                Board = post.Board.Url ?? "",
                Title = post.Title ?? "",
                CreatedBy = post.CreatedBy.Username ?? "",
                CreatedDateTime = post.CreatedDateTime,
                Content = post.Content,
                Url = post.Url,
                CommentCount = 0
            };
        }
    }
}
