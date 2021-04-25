using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Data;
using Postie.Api.Mappers;
using Postie.Api.Models.Db;
using Postie.Api.Repositories;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IFetchPostService _fetchPostService;

        private readonly IBoardRepository _boardRepository;
        
        private readonly PostResponseMapper _postResponseMapper;

        public PostsController(ApplicationDbContext dbContext,
                               IFetchPostService fetchPostService,
                               IBoardRepository boardRepository,
                               PostResponseMapper postResponseMapper)
        {
            _dbContext = dbContext;
            _fetchPostService = fetchPostService;
            _boardRepository = boardRepository;
            _postResponseMapper = postResponseMapper;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult GetPosts(string board, string post)
        {
            var boardProvided = !string.IsNullOrWhiteSpace(board);
            var postProvided = !string.IsNullOrWhiteSpace(post);
            
            // If we have neither board, nor post, bad request
            if (!boardProvided && !postProvided)
            {
                return BadRequest();
            }

            // If we have just board, return posts for that board
            if (boardProvided && !postProvided)
            {
                return Json(GetPostsForBoard(board));
            }

            // If we have a post but no board, bad request
            if (!boardProvided)
            {
                return BadRequest();
            }

            // If we have both, fetch that specific post
            var p = _fetchPostService.GetPostByBoardAndUrl(board, post);
            return p == null ? (IActionResult) NotFound() : Json(p);
        }

        [HttpGet]
        [Route("board")]
        public IActionResult GetPostsForBoard(string name)
        {
            var board = _boardRepository.GetBoardByName(name);
            var posts = _fetchPostService.GetPostsForBoard(board);

            return Json(_postResponseMapper.MapDbToResponseList(posts));
        }
    }
}