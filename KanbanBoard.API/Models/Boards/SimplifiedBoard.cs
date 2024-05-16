using KanbanBoard.API.Models.Columns;

namespace KanbanBoard.API.Models.Boards
{
    public class SimplifiedBoard
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<SimplifiedColumn> Columns { get; set; } = new List<SimplifiedColumn>();
    }
}
