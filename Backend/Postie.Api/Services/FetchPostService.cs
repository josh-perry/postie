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
    }
}
