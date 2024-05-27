using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;

namespace KanbanBoard.DataAccess.Repositories
{
    public class SubTaskRepository : GenericRepository<SubTask>, ISubTaskRepository
    {
        public SubTaskRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<SubTask> GetAllIncludes()
        {
            throw new NotImplementedException();
        }

        public SubTask? GetOneIncludes(int id)
        {
            throw new NotImplementedException();
        }
    }
}
