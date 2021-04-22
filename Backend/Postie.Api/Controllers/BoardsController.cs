using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Postie.Api.Data;

namespace Postie.Api.Controllers
{
    [ApiController]
    [Route("board")]
    public class BoardsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        
        public BoardsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        [HttpGet]
        [Route("")]
        public IActionResult GetBoard([FromQuery]string title)
        {
            // If no title is provided, get all
            if (string.IsNullOrEmpty(title))
            {
                return Json(_dbContext.Boards.ToList());
            }

            var board = _dbContext.Boards.FirstOrDefault(x => x.Title == title);
            return board == null ? (IActionResult) NotFound() : Json(board);
        }
    }
}
