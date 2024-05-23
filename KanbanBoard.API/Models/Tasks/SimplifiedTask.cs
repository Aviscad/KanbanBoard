using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Models.Tasks
{
    public class SimplifiedTask
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
