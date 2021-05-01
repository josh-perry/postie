using System.Collections.Generic;
using Postie.Api.Models.Db;
using Postie.Api.Models.Responses;

namespace Postie.Api.Mappers
{
    public class CommentResponseMapper
    {
        public IEnumerable<CommentApiResponse> MapDbToResponseList(IEnumerable<Comment> comments)
        {
            var result = new List<CommentApiResponse>();

            foreach (var comment in comments)
                result.Add(MapDbToResponse(comment));

            return result;
        }

        public CommentApiResponse MapDbToResponse(Comment board)
        {
            return new CommentApiResponse
            {
                Content = board.Content,
                User = board.CreatedBy.Username,
                ParentCommentId = board.ParentComment?.ID
            };
        }
    }
}
