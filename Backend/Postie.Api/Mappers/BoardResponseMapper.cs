using System.Collections.Generic;
using System.Linq;
using Postie.Api.Models.Db;
using Postie.Api.Models.Responses;

namespace Postie.Api.Mappers
{
    public class BoardResponseMapper
    {
        public IEnumerable<BoardApiResponse> MapDbToResponseList(IEnumerable<Board> boards)
        {
            return boards.Select(MapDbToResponse).ToList();
        }

        public BoardApiResponse MapDbToResponse(Board board)
        {
            return new BoardApiResponse
            {
                Title = board.Title ?? "",
                CreatedBy = board.CreatedBy?.Username ?? "",
                CreatedDateTime = board.CreatedDateTime,
                Url = board.Url,
                Description = board.Description
            };
        }
    }
}
