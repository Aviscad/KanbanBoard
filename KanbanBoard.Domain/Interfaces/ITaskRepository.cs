using Task = KanbanBoard.Domain.Entities.Task;

namespace KanbanBoard.Domain.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        IEnumerable<Task> GetAllIncludes();
    }
}
