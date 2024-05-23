using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Models.Tasks
{
    public class TaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ColumnId { get; set; }
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
