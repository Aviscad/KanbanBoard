using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KanbanBoard.DataAccess.Repositories
{
    public class BoardRepository : GenericRepository<Board>, IBoardRepository
    {
        public BoardRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<Board> GetAllIncludes(Expression<Func<Board, bool>> predicate)
        {
            return _context.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Tasks)
                .ThenInclude(t => t.SubTasks)
                .Where(predicate)
                .ToList();
        }

        public Board? GetOneIncludes(int id, Expression<Func<Board, bool>> predicate)
        {
            return _context.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Tasks)
                .ThenInclude(t => t.SubTasks)
                .Where(predicate)
                .FirstOrDefault(b => b.BoardId == id);
        }
    }
}
