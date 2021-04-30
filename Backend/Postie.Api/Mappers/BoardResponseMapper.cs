using System.Collections.Generic;
using Postie.Api.Models.Db;
using Postie.Api.Models.Responses;

namespace Postie.Api.Mappers
{
    public class BoardResponseMapper
    {
        public IEnumerable<BoardApiResponse> MapDbToResponseList(IEnumerable<Board> boards)
        {
            var result = new List<BoardApiResponse>();

            foreach (var board in boards)
                result.Add(MapDbToResponse(board));

            return result;
        }

        public BoardApiResponse MapDbToResponse(Board board)
        {
            return new BoardApiResponse
            {
                Title = board.Title ?? "",
                CreatedBy = board.CreatedBy?.Username ?? "",
                CreatedDateTime = board.CreatedDateTime
            };
        }
    }
}
