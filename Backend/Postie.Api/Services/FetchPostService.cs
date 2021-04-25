using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Postie.Api.Data;
using Postie.Api.Models.Db;

namespace Postie.Api.Services
{

    public interface IFetchPostService
    {
        IEnumerable<Post> GetPostsForBoard(Board board);

        Post GetPostByBoardAndUrl(string boardUrl, string postUrl);
    }
    
    public class FetchPostService : IFetchPostService
    {
        private readonly ApplicationDbContext _dbContext;
        
        public FetchPostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Post> GetPostsForBoard(Board board)
        {
            return _dbContext.Posts
                .Include(x => x.CreatedBy)
                .Where(x => x.Board == board);
        }

        public Post GetPostByBoardAndUrl(string boardUrl, string postUrl)
        {
            var board = _dbContext.Boards.FirstOrDefault(x => x.Title == boardUrl);
            var post = _dbContext.Posts
                .Include(x => x.CreatedBy)
                .FirstOrDefault(x => x.Board == board && x.Url == postUrl);

            return post;
        }
    }
}
