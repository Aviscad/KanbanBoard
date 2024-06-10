using KanbanBoard.Domain.Entities;
using System.Linq.Expressions;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IColumnRepository : IGenericRepository<Column>
    {
        IEnumerable<Column> GetAllIncludes(Expression<Func<Column, bool>> predicate);
        Column? GetOneIncludes(int id, Expression<Func<Column, bool>> predicate);
    }
}
