using Microsoft.AspNetCore.Identity;

namespace KanbanBoard.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Board> Board { get; set; }
    }
}
