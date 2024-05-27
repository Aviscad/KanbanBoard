using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.DataAccess.Repositories
{
    public class ColumnRepository : GenericRepository<Column>, IColumnRepository
    {
        public ColumnRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<Column> GetAllIncludes()
        {
            return _context.Columns.Include(c => c.Tasks).ToList();
        }

        public Column? GetOneIncludes(int id)
        {
            return _context.Columns.Include(c => c.Tasks).FirstOrDefault(c => c.ColumnId == id);
        }
    }
}
