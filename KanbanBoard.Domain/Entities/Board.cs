namespace KanbanBoard.Domain.Entities
{
    public class Board
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Column> Columns { get; set; } = new List<Column>();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
