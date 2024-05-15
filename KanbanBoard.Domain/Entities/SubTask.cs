using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Domain.Entities
{
    public class SubTask
    {
        public int SubTaskId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}
