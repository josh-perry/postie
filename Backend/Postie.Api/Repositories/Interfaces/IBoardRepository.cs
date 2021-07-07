using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        Board GetBoardByUrl(string boardName);

        bool AddBoard(Board board);

        IEnumerable<Board> GetAllBoards();
    }
}
