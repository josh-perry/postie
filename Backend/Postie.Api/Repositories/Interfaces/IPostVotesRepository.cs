using Postie.Api.Controllers;
using Postie.Api.Models;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories.Interfaces
{
    public interface IPostVotesRepository
    {
        PostVotes GetPostVotes(int postId);

        bool PostVote(Post post, User user, PostVoteRequest request);
    }

}
