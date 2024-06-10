using KanbanBoard.Domain.Entities;
using System.Linq.Expressions;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IBoardRepository : IGenericRepository<Board>
    {
        IEnumerable<Board> GetAllIncludes(Expression<Func<Board, bool>> predicate);
        Board? GetOneIncludes(int id, Expression<Func<Board, bool>> predicate);
    }
}
