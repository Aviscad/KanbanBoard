using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = KanbanBoard.Domain.Entities.Task;

namespace KanbanBoard.DataAccess.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<Task> GetAllIncludes()
        {
            return _context.Tasks
                .Include(t => t.Column)
                .Include(st => st.SubTasks)
                .ToList();
        }

        public Task? GetOneIncludes(int id)
        {
            return _context.Tasks
                .Include(t => t.Column)
                .Include(st => st.SubTasks)
                .FirstOrDefault(t => t.TaskId == id);
        }
    }
}
