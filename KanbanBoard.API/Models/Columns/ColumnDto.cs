using KanbanBoard.Domain.Entities;
using Task = KanbanBoard.Domain.Entities.Task;

namespace KanbanBoard.API.Models.Columns
{
    public class ColumnDto
    {
        public int ColumnId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BoardId { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
