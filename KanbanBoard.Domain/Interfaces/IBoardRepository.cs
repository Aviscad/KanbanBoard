using KanbanBoard.Domain.Entities;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IBoardRepository : IGenericRepository<Board>
    {
        IEnumerable<Board> GetAllIncludes();
        Board? GetOneIncludes(int id);
    }
}
