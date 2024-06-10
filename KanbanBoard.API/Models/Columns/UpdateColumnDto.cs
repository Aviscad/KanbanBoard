using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Columns
{
    public class UpdateColumnDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
    }
}
