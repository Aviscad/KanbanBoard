using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Models.Boards
{
    public class BoardDto
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Column> Columns { get; set; } = new List<Column>();
    }
}
