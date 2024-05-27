using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.DataAccess.Repositories
{
    public class SubTaskRepository : GenericRepository<SubTask>, ISubTaskRepository
    {
        public SubTaskRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<SubTask> GetAllIncludes()
        {
            return _context.SubTasks.Include(st => st.Task).ToList();
        }

        public SubTask? GetOneIncludes(int id)
        {
            return _context.SubTasks.Include(st => st.Task).FirstOrDefault(st => st.SubTaskId == id);
        }
    }
}
