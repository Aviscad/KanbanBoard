using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KanbanBoard.DataAccess.Repositories
{
    public class ColumnRepository : GenericRepository<Column>, IColumnRepository
    {
        public ColumnRepository(KanbanDbContext context) : base(context)
        {
        }

        public IEnumerable<Column> GetAllIncludes(Expression<Func<Column, bool>> predicate)
        {
            return _context.Columns
                .Include(c => c.Tasks)
                .ThenInclude(st => st.SubTasks)
                .Where(predicate)
                .ToList();
        }

        public Column? GetOneIncludes(int id, Expression<Func<Column, bool>> predicate)
        {
            return _context.Columns
                .Include(c => c.Tasks)
                .ThenInclude(st => st.SubTasks)
                .Where(predicate)   
                .FirstOrDefault(c => c.ColumnId == id);
        }
    }
}
