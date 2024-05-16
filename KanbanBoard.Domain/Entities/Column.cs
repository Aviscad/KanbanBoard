namespace KanbanBoard.Domain.Entities
{
    public class Column
    {
        public int ColumnId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
