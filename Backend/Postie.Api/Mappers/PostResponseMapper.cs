using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Mappers
{
    public class PostResponseMapper
    {
        public IEnumerable<PostApiResponse> MapDbToResponseList(IEnumerable<Post> posts)
        {
            var result = new List<PostApiResponse>();

            foreach (var post in posts)
                result.Add(MapDbToResponse(post));

            return result;
        }

        public PostApiResponse MapDbToResponse(Post post)
        {
            return new PostApiResponse
            {
                Board = post.Board.Title ?? "",
                Title = post.Title ?? "",
                CreatedBy = post.CreatedBy.Username ?? "",
                CreatedDateTime = post.CreatedDateTime,
                Content = post.Content,
                Url = post.Url
            };
        }
    }
}
