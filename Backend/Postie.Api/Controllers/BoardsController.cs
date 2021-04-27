using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpGet]
        [Route("")]
        public IActionResult Get(string title)
        {
            // If no title is provided, get all
            if (string.IsNullOrEmpty(title))
            {
                return Json(_dbContext.Boards.ToList());
            }

            var board = _dbContext.Boards.FirstOrDefault(x => x.Title == title);
            return board == null ? (IActionResult) NotFound() : Json(board);
        }
        
        [HttpPut]
        [Authorize]
        [Route("")]
        public IActionResult Put(NewBoard board)
        {
            // Validate this board doesn't already exist
            if (_boardRepository.GetBoardByName(board.Title) != null)
            {
                return Conflict($"{board.Title} already exists!");
            }

            var boardUrl = _urlService.GenerateUrl(board.Title);
            var user = _userRepository.GetUserByAuthId(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var success = _boardRepository.AddBoard(new Board
            {
                Title = board.Title,
                CreatedBy = user,
                CreatedDateTime = DateTime.Now
            });

            if (success)
            {
                return Created(nameof(Get), new {
                    boardUrl = boardUrl
                });
            }

            return Problem("Failed to create board");
        }
    }
}
