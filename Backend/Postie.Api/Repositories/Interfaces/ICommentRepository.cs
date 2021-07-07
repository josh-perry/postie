using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsForPost(string board, string post, int childrenOf = default);

        bool AddComment(Comment comment);

        Comment GetCommentById(int id);

        IEnumerable<Comment> GetLastCommentsByUser(User user, int amount);

        int GetCommentsCountForPostId(int postId);
    }
}
