using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BoardsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get() { 
            var boards = _unitOfWork.Board.GetAll();
            return Ok(boards);
        }
    }
}
