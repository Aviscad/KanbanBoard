using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Domain.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ColumnId { get; set; }
        public Column Column { get; set; }
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
