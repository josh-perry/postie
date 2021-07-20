using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Models.Db;
using Postie.Api.Models.Requests;
using Postie.Api.Repositories.Interfaces;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostsController : Controller
    {
        private readonly IBoardRepository _boardRepository;

        private readonly IPostRepository _postRepository;

        private readonly IUrlService _urlService;

        private readonly IUserRepository _userRepository;

        private readonly PostResponseMapper _postResponseMapper;

        public PostsController(IPostRepository postRepository,
            IBoardRepository boardRepository,
            PostResponseMapper postResponseMapper,
            IUrlService urlService,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _boardRepository = boardRepository;
            _postResponseMapper = postResponseMapper;
            _urlService = urlService;
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Get post details by board and post
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <param name="postUrl"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">The post cannot be found</response>
        [HttpGet]
        [Route("board/{boardUrl}/{postUrl}")]
        public IActionResult GetByBoardAndPost(string boardUrl, string postUrl)
        {
            var post = _postRepository.GetPostByBoardAndUrl(boardUrl, postUrl);

            if (post == null)
                return NotFound();

            var response = _postResponseMapper.MapDbToResponse(post);
            return Json(response);
        }

        /// <summary>
        ///     Get a list of posts for a board
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <returns></returns>
        /// <response code="200">The posts</response>
        /// <response code="404">The board cannot be found</response>
        [HttpGet]
        [Route("board/{boardUrl}")]
        public IActionResult GetByBoard(string boardUrl)
        {
            var board = _boardRepository.GetBoardByUrl(boardUrl);

            if (board == null)
                return NotFound();

            var posts = _postRepository.GetPostsForBoard(board);
            var response = _postResponseMapper.MapDbToResponseList(posts);
            return Json(response);
        }

        /// <summary>
        ///     Add a new post
        /// </summary>
        /// <param name="boardUrl"></param>
        /// <param name="addPostRequest"></param>
        /// <returns></returns>
        /// <response code="200">The url of the new post</response>
        /// <response code="400">Url/request mismatch</response>
        /// <response code="404">Board cannot be found</response>
        /// <response code="500"></response>
        [HttpPost]
        [Authorize]
        [Route("board/{boardUrl}")]
        public IActionResult Post(string boardUrl, AddPostRequest addPostRequest)
        {
            if (boardUrl != addPostRequest.Board)
                return BadRequest("Board in URL and board in request mismatch!");

            var user = _userRepository.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (user == null)
                return BadRequest("User is null.");

            var board = _boardRepository.GetBoardByUrl(boardUrl);

            if (board == null)
                return NotFound("Board not found");

            // TODO: check if this conflicts
            var postUrl = _urlService.GenerateUrl(addPostRequest.Title);

            var post = new Post
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Board = board,
                Url = postUrl,
                CreatedDateTime = DateTime.UtcNow,
                CreatedBy = user
            };

            _postRepository.AddPost(post);

            // TODO: fix this. We should just be able to return CreatedAtAction(nameof(...)) but it
            //       doesn't seem to get the route properly.
            var baseUrl = new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}");
            var createdAtUrl = new Uri(baseUrl, $"/post/board/{board.Url}/{post.Url}");
            return Created(createdAtUrl, _postResponseMapper.MapDbToResponse(post));
        }

        /// <summary>
        ///     Gets the top posts
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("top")]
        public IActionResult GetTop(int skip = 0, int take = 10)
        {
            var posts = _postRepository.GetTopPosts(skip, take);
            var response = _postResponseMapper.MapDbToResponseList(posts);
            return Json(response);
        }
    }
}
