using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Boards
{
    public class UpdateBoardDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
    }
}
