using KanbanBoard.API.Models.Boards;
using KanbanBoard.API.Models.Columns;
using KanbanBoard.API.Models.Tasks;
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
                Tasks = column.Tasks.Select(c =>
                    new SimplifiedTask
                    {
                        TaskId = c.TaskId,
                        Title = c.Title,
                        Description = c.Description,
                        ColumnId = column.ColumnId,
                        ColumnName = column.Name,
                    })
            };
        }

        public static List<SimplifiedColumn> ToSimplifiedColumnList(this IEnumerable<Column> columns)
        {
            return columns.Select(column => new SimplifiedColumn
            {
                ColumnId = column.ColumnId,
                Name= column.Name,  
                Tasks = column.Tasks.Select(c =>
                    new SimplifiedTask
                    {
                        TaskId = c.TaskId,
                        Title = c.Title,
                        Description = c.Description,
                        ColumnId = column.ColumnId,
                        ColumnName = column.Name,
                    })
            }).ToList();
        }

        public static Column ToColumn(this CreateColumnDto createColumnDto)
        {
            return new Column
            {
                Name = createColumnDto.Name,
                BoardId = createColumnDto.BoardId,
            };
        }

        public static Column ToColumn(this UpdateColumnDto updateColumnDto)
        {
            return new Column
            {
                Name = updateColumnDto.Name,
                BoardId = updateColumnDto.BoardId,
            };
        }
    }
}
