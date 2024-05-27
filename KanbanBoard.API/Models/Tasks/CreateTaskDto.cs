using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int ColumnId { get; set; }
    }
}
