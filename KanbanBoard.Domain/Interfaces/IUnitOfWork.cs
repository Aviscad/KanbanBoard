using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBoardRepository Board { get; }
        IColumnRepository Column { get; }
        ITaskRepository Task { get; }
        ISubTaskRepository SubTask { get; }
        int Save();
    }
}
