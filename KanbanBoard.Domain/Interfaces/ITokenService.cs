using KanbanBoard.Domain.Entities;

namespace KanbanBoard.Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
