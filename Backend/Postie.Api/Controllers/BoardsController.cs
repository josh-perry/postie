using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
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
        private readonly IBoardRepository _boardRepository;

        private readonly BoardResponseMapper _boardResponseMapper;

        private readonly IUrlService _urlService;

        private readonly IUserRepository _userRepository;

        public BoardsController(IBoardRepository boardRepository,
            IUrlService urlService,
            IUserRepository userRepository,
            BoardResponseMapper boardResponseMapper)
        {
            _boardRepository = boardRepository;
            _urlService = urlService;
            _userRepository = userRepository;
            _boardResponseMapper = boardResponseMapper;
        }

        /// <summary>
        ///     Get a list of all boards
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var boards = _boardRepository.GetAllBoards();
            return Json(_boardResponseMapper.MapDbToResponseList(boards));
        }

        /// <summary>
        ///     Get an existing board
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
            var b = _boardRepository.GetBoardByUrl(board);

            if (b == null)
                return NotFound();

            return Json(_boardResponseMapper.MapDbToResponse(b));
        }

        /// <summary>
        ///     Adds a new board
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
            if (_boardRepository.GetBoardByUrl(newBoard.Title) != null)
                return Conflict($"{newBoard.Title} already exists!");

            var user = _userRepository.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var success = _boardRepository.AddBoard(new Board
            {
                Url = board,
                Title = newBoard.Title,
                CreatedBy = user,
                CreatedDateTime = DateTime.UtcNow
            });

            if (!success)
                return Problem("Failed to create board");
            
            return CreatedAtAction(nameof(Get), new
            {
                board
            });
        }
    }
}
