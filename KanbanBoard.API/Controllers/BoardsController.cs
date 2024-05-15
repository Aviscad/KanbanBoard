using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Boards;
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

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id) {
            var board = _unitOfWork.Board.GetById(id);
            if(board == null) return NotFound();
            return Ok(board);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBoardDto boardDto)
        {
            if(boardDto == null) return BadRequest();

            var boardModel = boardDto.ToCreateBoardDto();
            await _unitOfWork.Board.AddAsync(boardModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = boardModel.BoardId }, boardModel.ToBoardDto());
        }

    }
}
