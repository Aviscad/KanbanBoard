using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int ColumnId { get; set; }
    }
}
