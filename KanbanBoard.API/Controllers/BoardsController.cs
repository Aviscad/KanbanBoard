using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Boards;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KanbanBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BoardsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var boards = _unitOfWork.Board
                .GetAllIncludes()
                .ToSimplifiedBoardList();
            return Ok(boards);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var board = _unitOfWork.Board.GetOneIncludes(id);
            if (board == null) return NotFound();
            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBoardDto boardDto)
        {
            if (boardDto == null) return BadRequest();

            var boardModel = boardDto.ToBoard();
            await _unitOfWork.Board.AddAsync(boardModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = boardModel.BoardId }, boardModel.ToSimplifiedBoard());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBoardDto boardDto)
        {
            var boardModel = await _unitOfWork.Board.GetByIdAsync(id);
            if (boardModel == null) return NotFound();

            boardModel.Name = boardDto.Name;

            _unitOfWork.Board.Update(boardModel);
            await _unitOfWork.SaveAsync();
            return Ok(boardModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();
            var boardModel = await _unitOfWork.Board.GetByIdAsync(id);

            if (boardModel == null) return NotFound();

            _unitOfWork.Board.Remove(boardModel);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
