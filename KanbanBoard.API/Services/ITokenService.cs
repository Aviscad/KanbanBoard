using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
