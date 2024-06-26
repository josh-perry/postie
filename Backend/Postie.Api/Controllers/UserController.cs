using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        private readonly CommentResponseMapper _commentResponseMapper;

        private readonly IPostRepository _postRepository;

        private readonly PostResponseMapper _postResponseMapper;

        private readonly IUserRepository _userRepository;

        private readonly UserResponseMapper _userResponseMapper;

        public UserController(IUserRepository userRepository,
            ICommentRepository commentRepository,
            UserResponseMapper userResponseMapper,
            CommentResponseMapper commentResponseMapper,
            PostResponseMapper postResponseMapper,
            IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _userResponseMapper = userResponseMapper;
            _commentResponseMapper = commentResponseMapper;
            _postResponseMapper = postResponseMapper;
            _postRepository = postRepository;
        }

        /// <summary>
        ///     Retrieves the claims of the logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("")]
        public IActionResult GetMe()
        {
            var user = _userRepository.GetUserByUsername(User.FindFirst(ClaimTypes.Name)?.Value);

            if (user == null)
                return BadRequest("User is null.");

            return Json(_userResponseMapper.MapDbToResponse(user));
        }

        /// <summary>
        ///     Gets details about a given user
        /// </summary>
        /// <param name="username">The username to fetch details about</param>
        /// <param name="recentCommentsCount">How many recent comments should we grab?</param>
        /// <param name="recentPostsCount">How many recent posts should we grab?</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{username}")]
        public IActionResult GetByUsername(string username, int recentCommentsCount = 10, int recentPostsCount = 10)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user == null)
                return NotFound();

            var lastComments = _commentRepository.GetLastCommentsByUser(user, recentCommentsCount);
            var lastPosts = _postRepository.GetLastPostsByUser(user, recentPostsCount);

            var userResponse = _userResponseMapper.MapDbToResponse(user);
            userResponse.RecentComments = _commentResponseMapper.MapDbToResponseList(lastComments);
            userResponse.RecentPosts = _postResponseMapper.MapDbToResponseList(lastPosts);

            foreach (var post in userResponse.RecentPosts)
                post.CommentCount = _commentRepository.GetCommentsCountForPostId(post.ID);

            return Json(userResponse);
        }
    }
}
