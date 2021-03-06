using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Postie.Api.Data;
using Postie.Api.Models.Db;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Comment> GetCommentsForPost(string board, string post, int childrenOf = default)
        {
            // TODO: this needs to be improved, I think it'll fail if there are 2 posts with the same URL on
            //       different boards.
            if (childrenOf == default)
                return _dbContext.Comments
                    .Where(x => x.Post.Url == post)
                    .Include(x => x.CreatedBy)
                    .ToList();

            return _dbContext.Comments
                .Where(x => x.Post.Url == post)
                .Where(x => x.ParentComment.ID == childrenOf)
                .Include(x => x.CreatedBy)
                .ToList();
        }

        public bool AddComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            return _dbContext.SaveChanges() >= 0;
        }

        public Comment GetCommentById(int id)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Comment> GetLastCommentsByUser(User user, int amount)
        {
            return _dbContext.Comments
                .Include(x => x.Post)
                .ThenInclude(x => x.Board)
                .Where(x => x.CreatedBy == user)
                .OrderByDescending(x => x.CreatedDateTime)
                .Take(amount)
                .ToList();
        }

        public int GetCommentsCountForPostId(int postId)
        {
            return _dbContext.Comments.Count(x => x.Post.ID == postId);
        }
    }
}
