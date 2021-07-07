using System.Linq;
using Postie.Api.Controllers;
using Postie.Api.Data;
using Postie.Api.Models;
using Postie.Api.Models.Db;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Repositories
{
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

        public bool PostVote(Post post, User user, PostVoteRequest request)
        {
            // Check if this user has voted on this post before and grab that vote if so
            var vote = _dbContext.PostVotes
                .FirstOrDefault(x => x.Post == post && x.User == user);

            if (vote != null)
            {
                // If we have ane existing vote and it's the same as the request then
                // just pretend we did it and return early.
                if (vote.Up == request.Up)
                    return true;

                vote.Up = request.Up;
                _dbContext.Update(vote);
                return _dbContext.SaveChanges() > 0;
            }

            vote = new PostVote
            {
                Post = post,
                Up = request.Up,
                User = user
            };

            _dbContext.Add(vote);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
