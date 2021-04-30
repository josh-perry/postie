using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Data;
using Postie.Api.Models.Db;
using Postie.Api.Repositories;
using Postie.Api.Services;

namespace Postie.Api.Controllers
{
    public class NewBoard
    {
        public string Title { get; set; }
    }
    
    [ApiController]
    [Route("board")]
    public class BoardsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IBoardRepository _boardRepository;
        
        private readonly IUrlService _urlService;
        private readonly IUserRepository _userRepository;

        public BoardsController(ApplicationDbContext dbContext, IBoardRepository boardRepository, IUrlService urlService, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _boardRepository = boardRepository;
            _urlService = urlService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get a list of all boards
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var boards = _boardRepository.GetAllBoards();
            return Json(boards);
        }
        
        /// <summary>
        /// Get an existing board
        /// </summary>
        /// <param name="board">Board name</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">The board cannot be found</response>
        [HttpGet]
        [Route("{board}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string board)
        {
            // If no title is provided, get all
            if (string.IsNullOrEmpty(board))
            {
                return Json(_dbContext.Boards.ToList());
            }

            var b = _dbContext.Boards.FirstOrDefault(x => x.Title == board);
            return b == null ? (IActionResult) NotFound() : Json(b);
        }
        
        /// <summary>
        /// Adds a new board
        /// </summary>
        /// <param name="board">Board name</param>
        /// <param name="newBoard">New board details</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{board}")]
        public IActionResult Put(string board, NewBoard newBoard)
        {
            // Validate this board doesn't already exist
            if (_boardRepository.GetBoardByName(newBoard.Title) != null)
            {
                return Conflict($"{newBoard.Title} already exists!");
            }

            var user = _userRepository.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            board = _urlService.GenerateUrl(board);

            var success = _boardRepository.AddBoard(new Board
            {
                Title = board,
                CreatedBy = user,
                CreatedDateTime = DateTime.Now
            });

            if (success)
            {
                return CreatedAtAction(nameof(Get), new
                {
                    board = board
                });
            }

            return Problem("Failed to create board");
        }
    }
}
