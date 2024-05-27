using KanbanBoard.API.Models.Tasks;

namespace KanbanBoard.API.Models.Columns
{
    public class SimplifiedColumn
    {
        public int ColumnId { get; set; }
        public string Name { get; set; } = string.Empty;

        public IEnumerable<SimplifiedTask> Tasks { get; set; } = new List<SimplifiedTask>();
    }
}
