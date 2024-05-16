namespace KanbanBoard.API.Models.Columns
{
    public class UpdateColumnDto
    {
        public string Name { get; set; } = string.Empty;
        public int BoardId { get; set; }
    }
}
