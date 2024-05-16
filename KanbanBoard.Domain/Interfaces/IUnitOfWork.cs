namespace KanbanBoard.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBoardRepository Board { get; }
        IColumnRepository Column { get; }
        ITaskRepository Task { get; }
        ISubTaskRepository SubTask { get; }
        int Save();
        Task SaveAsync();
    }
}
