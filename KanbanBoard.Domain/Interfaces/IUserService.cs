using KanbanBoard.Domain.Entities;
using System.Security.Claims;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IUserService
    {
        string GetId(ClaimsPrincipal principal);
    }
}
