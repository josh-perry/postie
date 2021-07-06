using System.Collections.Generic;
using System.Linq;
using Postie.Api.Models.Db;
using Postie.Api.Models.Responses;

namespace Postie.Api.Mappers
{
    public class CommentResponseMapper
    {
        public IEnumerable<CommentApiResponse> MapDbToResponseList(IEnumerable<Comment> comments)
        {
            return comments.Select(MapDbToResponse).ToList();
        }

        public CommentApiResponse MapDbToResponse(Comment board)
        {
            return new CommentApiResponse
            {
                ID = board.ID,
                Content = board.Content,
                User = board.CreatedBy.Username,
                ParentCommentId = board.ParentComment?.ID,
                Children = new List<Comment>(),
                Board = board?.Post?.Board.Url,
                Post = board?.Post?.Url
            };
        }
    }
}
