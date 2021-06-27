using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Repositories;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        private readonly ICommentRepository _commentRepository;
        
        private readonly UserResponseMapper _userResponseMapper;
        
        private readonly CommentResponseMapper _commentResponseMapper;
        
        private readonly PostResponseMapper _postResponseMapper;

        private readonly IFetchPostService _fetchPostService;

        public UserController(IUserRepository userRepository,
            ICommentRepository commentRepository,
            UserResponseMapper userResponseMapper,
            CommentResponseMapper commentResponseMapper,
            PostResponseMapper postResponseMapper,
            IFetchPostService fetchPostService)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _userResponseMapper = userResponseMapper;
            _commentResponseMapper = commentResponseMapper;
            _postResponseMapper = postResponseMapper;
            _fetchPostService = fetchPostService;
        }
        
        [HttpGet]
        [Authorize]
        [Route("")]
        public IActionResult Get()
        {
            var claims = HttpContext.User.Claims.Select(x => x.Value);
            return Json(claims);
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult Get(string username, int recentCommentsCount = 10, int recentPostsCount = 10)
        {
            var user = _userRepository.GetUserByName(username);

            if (user == null)
            {
                return NotFound();
            }

            var lastComments = _commentRepository.GetLastCommentsByUser(user, recentCommentsCount);
            var lastPosts = _fetchPostService.GetLastPostsByUser(user, recentPostsCount);
            
            var userResponse = _userResponseMapper.MapDbToResponse(user);
            userResponse.RecentComments = _commentResponseMapper.MapDbToResponseList(lastComments);
            userResponse.RecentPosts = _postResponseMapper.MapDbToResponseList(lastPosts);
            
            return Json(userResponse);
        }
    }
}
