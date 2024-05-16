namespace KanbanBoard.API.Models.Columns
{
    public class CreateColumnDto
    {
        public string Name { get; set; } = string.Empty;
        public int BoardId { get; set; }
    }
}
