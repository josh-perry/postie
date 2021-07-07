using System.Linq;
using Postie.Api.Data;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories
{
    public class PostVotes
    {
        public int PostId { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }
    }
    
    public interface IPostVotesRepository
    {
        PostVotes GetPostVotes(int postId);
    }

    public class PostVotesRepository : IPostVotesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public PostVotesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PostVotes GetPostVotes(int postId)
        {
            var up = _dbContext.PostVotes.Count(x => x.Post.ID == postId && x.Up);
            var down = _dbContext.PostVotes.Count(x => x.Post.ID == postId && !x.Up);

            return new PostVotes
            {
                PostId = postId,
                UpVotes = up,
                DownVotes = down
            };
        }
    }
}
