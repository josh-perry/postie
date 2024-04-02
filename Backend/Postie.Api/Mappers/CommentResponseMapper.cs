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

        public CommentApiResponse MapDbToResponse(Comment comment)
        {
            return new CommentApiResponse
            {
                ID = comment.ID,
                Content = comment.Content,
                User = comment.CreatedBy.UserName,
                ParentCommentId = comment.ParentComment?.ID,
                Children = new List<Comment>(),
                Board = comment.Post?.Board.Url,
                Post = comment.Post?.Url,
                CreatedDateTime = comment.CreatedDateTime
            };
        }
    }
}
