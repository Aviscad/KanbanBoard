using KanbanBoard.API.Models.Boards;
using KanbanBoard.API.Models.Columns;
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

        public static SimplifiedBoard ToSimplifiedBoard(this Board board)
        {
            return new SimplifiedBoard
            {
                BoardId = board.BoardId,
                Name = board.Name,
                Columns = board.Columns.Select(c =>
                    new SimplifiedColumn
                    {
                        ColumnId = c.ColumnId,
                        Name = c.Name,
                    })
            };
        }

        public static List<SimplifiedBoard> ToSimplifiedBoardList(this IEnumerable<Board> boards)
        {
            return boards.Select(board => new SimplifiedBoard
            {
                BoardId = board.BoardId,
                Name = board.Name,
                Columns = board.Columns.Select(c =>
                    new SimplifiedColumn
                    {
                        ColumnId = c.ColumnId,
                        Name = c.Name,
                    })
            }).ToList();
        }

        public static Board ToBoard(this CreateBoardDto createBoardDto)
        {
            return new Board
            {
                Name = createBoardDto.Name,
            };
        }
    }
}
