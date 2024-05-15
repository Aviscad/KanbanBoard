using KanbanBoard.API.Models.Boards;
using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Mappers
{
    public static class BoardMapper
    {
        public static BoardDto ToBoardDto(this Board board)
        {
            return new BoardDto
            {
                BoardId = board.BoardId,
                Name = board.Name,
                Columns = (List<Column>)board.Columns
            };
        }
        public static Board ToCreateBoardDto(this CreateBoardDto createBoardDto)
        {
            return new Board
            {
                Name = createBoardDto.Name,
            };
        }

        public static Board ToUpdateBoardDto(this UpdateBoardDto updateBoardDto)
        {
            return new Board
            {
                Name = updateBoardDto.Name
            };
        }

    }
}
