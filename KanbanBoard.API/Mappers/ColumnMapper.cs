using KanbanBoard.API.Models.Columns;
using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Mappers
{
    public static class ColumnMapper
    {
        public static ColumnDto ToColumnDto(this Column column)
        {
            return new ColumnDto
            {
                Name = column.Name,
                BoardId = column.BoardId,
                Tasks = column.Tasks,
            };
        }
        public static SimplifiedColumn ToSimplifiedColumn(this Column column)
        {
            return new SimplifiedColumn
            {
                ColumnId = column.ColumnId,
                Name = column.Name,
            };
        }

        public static Column ToColumn(this CreateColumnDto createColumnDto)
        {
            return new Column
            {
                Name = createColumnDto.Name,
                BoardId = createColumnDto.BoardId,
            };
        }
    }
}
