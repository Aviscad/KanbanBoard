using KanbanBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace KanbanBoard.DataAccess.Data
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
    }
}
