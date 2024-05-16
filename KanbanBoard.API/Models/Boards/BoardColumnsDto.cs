using KanbanBoard.API.Models.Columns;

namespace KanbanBoard.API.Models.Boards
{
    public class BoardColumnsDto
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<SelectColumnDto>? Columns { get; set; }
    }
}
