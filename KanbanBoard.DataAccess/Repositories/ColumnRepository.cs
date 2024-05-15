using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.DataAccess.Repositories
{
    public class ColumnRepository : GenericRepository<Column>, IColumnRepository
    {
        public ColumnRepository(KanbanDbContext context) : base(context)
        {
        }
    }
}
