using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Mappers;
using Postie.Api.Models.Db;
using Postie.Api.Models.Requests;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Controllers
{

    [ApiController]
    [Route("board")]
    public class BoardsController : Controller
    {
        private readonly IBoardRepository _boardRepository;

        private readonly BoardResponseMapper _boardResponseMapper;

        private readonly IUserRepository _userRepository;

        public BoardsController(IBoardRepository boardRepository,
            IUserRepository userRepository,
            BoardResponseMapper boardResponseMapper)
        {
            _boardRepository = boardRepository;
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
        public IActionResult GetAll()
        {
            var boards = _boardRepository.GetAllBoards();
            return Json(_boardResponseMapper.MapDbToResponseList(boards));
        }

        /// <summary>
        ///     Get an existing board
        /// </summary>
        /// <param name="boardUrl">Board name</param>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">The board cannot be found</response>
        [HttpGet]
        [Route("{boardUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByUrls(string boardUrl)
        {
            var b = _boardRepository.GetBoardByUrl(boardUrl);

            if (b == null)
                return NotFound();

            return Json(_boardResponseMapper.MapDbToResponse(b));
        }

        /// <summary>
        ///     Adds a new board
        /// </summary>
        /// <param name="boardUrl">Board name</param>
        /// <param name="newBoardRequest">New board details</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{boardUrl}")]
        public IActionResult Put(string boardUrl, NewBoardRequest newBoardRequest)
        {
            // Validate this board doesn't already exist
            if (_boardRepository.GetBoardByUrl(newBoardRequest.Title) != null)
                return Conflict($"{newBoardRequest.Title} already exists!");

            var user = _userRepository.GetUserByUsername(User.FindFirst(ClaimTypes.Name)?.Value);

            var success = _boardRepository.AddBoard(new Board
            {
                Url = boardUrl,
                Title = newBoardRequest.Title,
                CreatedBy = user,
                CreatedDateTime = DateTime.UtcNow,
                Description = newBoardRequest.Description
            });

            if (!success)
                return Problem("Failed to create board");

            return CreatedAtAction(nameof(GetAll), new
            {
                board = boardUrl
            });
        }
    }
}
