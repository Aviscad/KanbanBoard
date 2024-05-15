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
    public class SubTaskRepository : GenericRepository<SubTask>, ISubTaskRepository
    {
        public SubTaskRepository(KanbanDbContext context) : base(context)
        {
        }
    }
}
