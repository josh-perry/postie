using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Repositories.Interfaces;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("vote")]
    public class VotesController : Controller
    {

        private readonly IFetchPostService _fetchPostService;

        private readonly IPostVotesRepository _postVotesRepository;
        private readonly IUserRepository _userRepository;

        public VotesController(IUserRepository userRepository,
            IFetchPostService fetchPostService,
            IPostVotesRepository postVotesRepository)
        {
            _userRepository = userRepository;
            _fetchPostService = fetchPostService;
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
            var user = _userRepository.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (user == null)
                return BadRequest("User is null.");

            var post = _fetchPostService.GetPostById(postId);
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
