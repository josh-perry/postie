using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("vote")]
    public class VotesController : Controller
    {

        private readonly IPostRepository _postRepository;

        private readonly IPostVotesRepository _postVotesRepository;
        private readonly IUserRepository _userRepository;

        public VotesController(IUserRepository userRepository,
            IPostRepository postRepository,
            IPostVotesRepository postVotesRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _postVotesRepository = postVotesRepository;
        }

        /// <summary>
        ///     Add a vote to a post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("post/{postId}")]
        public IActionResult AddPostVote(int postId, PostVoteRequest postVoteRequest)
        {
            var user = _userRepository.GetUserByUsername(User.FindFirst(ClaimTypes.Name)?.Value);
            if (user == null)
                return BadRequest("User is null.");

            var post = _postRepository.GetPostById(postId);
            if (post == null)
                return BadRequest("Post is null.");

            _postVotesRepository.PostVote(post, user, postVoteRequest);
            return Json(_postVotesRepository.GetPostVotes(postId));
        }

        /// <summary>
        ///     Get a posts vote stats
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("post/{postId}")]
        public IActionResult GetPostVotes(int postId)
        {
            var votes = _postVotesRepository.GetPostVotes(postId);
            return Json(votes);
        }
    }
}
