using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Repositories;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostsController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        
        private readonly IFetchPostService _fetchPostService;

        private readonly PostResponseMapper _postResponseMapper;

        public PostsController(IFetchPostService fetchPostService,
            IBoardRepository boardRepository,
            PostResponseMapper postResponseMapper)
        {
            _fetchPostService = fetchPostService;
            _boardRepository = boardRepository;
            _postResponseMapper = postResponseMapper;
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
        public IActionResult Get(string boardUrl, string postUrl)
        {
            var post = _fetchPostService.GetPostByBoardAndUrl(boardUrl, postUrl);
            return post == null ? (IActionResult) NotFound() : Json(post);
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
        public IActionResult Get(string boardUrl)
        {
            var board = _boardRepository.GetBoardByName(boardUrl);

            if (board == null)
                return NotFound();

            var posts = _fetchPostService.GetPostsForBoard(board);
            return Json(_postResponseMapper.MapDbToResponseList(posts));
        }
    }
}
