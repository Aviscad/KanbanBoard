using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Account
{
    public class ForgotPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
