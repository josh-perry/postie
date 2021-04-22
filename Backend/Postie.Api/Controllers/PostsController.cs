using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Data;
using Postie.Api.Mappers;
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
        public IActionResult ListAll()
        {
            return Json(_dbContext.Posts.ToList());
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