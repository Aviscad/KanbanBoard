using KanbanBoard.Domain.Entities;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IColumnRepository : IGenericRepository<Column>
    {
        IEnumerable<Column> GetAllIncludes();
        Column? GetOneIncludes(int id);
    }
}
