using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Interfaces;

namespace KanbanBoard.DataAccess.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(KanbanDbContext context) : base(context)
        {
        }
    }
}
