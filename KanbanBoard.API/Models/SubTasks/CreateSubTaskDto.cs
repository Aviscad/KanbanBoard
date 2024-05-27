using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.SubTasks
{
    public class CreateSubTaskDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool Completed { get; set; }
        [Required]
        public int TaskId { get; set; }
    }
}
