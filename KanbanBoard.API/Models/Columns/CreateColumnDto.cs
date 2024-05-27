using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Columns
{
    public class CreateColumnDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int BoardId { get; set; }
    }
}
