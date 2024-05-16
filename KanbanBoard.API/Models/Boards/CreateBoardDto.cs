using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.API.Models.Boards
{
    public class CreateBoardDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
    }
}
