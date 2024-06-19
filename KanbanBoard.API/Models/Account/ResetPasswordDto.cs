﻿using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Account
{
    public class ResetPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required] public string NewPassword { get; set; } = string.Empty;
    }
}
