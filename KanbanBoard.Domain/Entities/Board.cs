using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Domain.Entities
{
    public class Board
    {
        public int BoardId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Column> Columns { get; set; } = new List<Column>();
    }
}
