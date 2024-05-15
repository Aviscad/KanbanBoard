using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.DataAccess.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(KanbanDbContext context) : base(context)
        {
        }
    }
}
