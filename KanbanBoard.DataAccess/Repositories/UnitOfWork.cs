using KanbanBoard.DataAccess.Data;
using KanbanBoard.Domain.Interfaces;

namespace KanbanBoard.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KanbanDbContext _context;

        public UnitOfWork(KanbanDbContext context)
        {
            _context = context;
            Board = new BoardRepository(_context);
            Column = new ColumnRepository(_context);
            Task = new TaskRepository(_context);
            SubTask = new SubTaskRepository(_context);
        }

        public IBoardRepository Board { get; }

        public IColumnRepository Column { get; }

        public ITaskRepository Task { get; }

        public ISubTaskRepository SubTask { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
