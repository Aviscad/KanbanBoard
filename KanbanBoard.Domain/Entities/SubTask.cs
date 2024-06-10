namespace KanbanBoard.Domain.Entities
{
    public class SubTask
    {
        public int SubTaskId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
    }
}
