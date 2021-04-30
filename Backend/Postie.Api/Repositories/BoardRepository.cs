using System.Collections.Generic;
using System.Linq;
using Postie.Api.Data;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BoardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Board GetBoardByName(string boardName)
        {
            return _dbContext.Boards.FirstOrDefault(x => x.Title == boardName);
        }
        
        public bool AddBoard(Board board)
        {
            _dbContext.Boards.Add(board);
            return _dbContext.SaveChanges() != 0;
        }
        public IEnumerable<Board> GetAllBoards()
        {
            return _dbContext.Boards.ToList();
        }
    }
}
