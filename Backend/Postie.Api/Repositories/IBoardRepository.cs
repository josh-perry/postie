using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories
{
    public interface IBoardRepository
    {
        Board GetBoardByName(string boardName);

        bool AddBoard(Board board);

        IEnumerable<Board> GetAllBoards();
    }
}
