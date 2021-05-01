using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Postie.Api.Data;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsForPost(string board, string post);

        bool AddComment(Comment comment);

        Comment GetCommentById(int id);
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Comment> GetCommentsForPost(string board, string post)
        {
            return _dbContext.Comments
                .Where(x => x.Post.Url == post)
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
    }
}
