using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.DataAccess.Repositories
{
    public class BoardRepository : GenericRepository<Board>, IBoardRepository
    {
        public BoardRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<Board> GetAllIncludes()
        {
            return _context.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Tasks)
                .ThenInclude(t => t.SubTasks)
                .ToList();
        }

        public Board? GetOneIncludes(int id)
        {
            return _context.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Tasks)
                .ThenInclude(t => t.SubTasks)
                .FirstOrDefault(b => b.BoardId == id);
        }
    }
}
