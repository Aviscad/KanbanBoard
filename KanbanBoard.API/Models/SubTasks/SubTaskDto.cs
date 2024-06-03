using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Models.SubTasks
{
    public class SubTaskDto
    {
        public int SubTaskId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public int TaskId { get; set; }
    }
}
