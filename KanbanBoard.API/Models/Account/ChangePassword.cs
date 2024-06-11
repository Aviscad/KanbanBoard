using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Account
{
    public class ChangePassword
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}
