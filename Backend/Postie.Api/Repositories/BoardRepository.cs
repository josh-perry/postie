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
    }
}
