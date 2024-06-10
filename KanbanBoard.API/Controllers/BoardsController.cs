using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Boards;
using KanbanBoard.Domain.Entities;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public BoardsController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var boards = _unitOfWork.Board
                .GetAllIncludes(u => u.UserId == userId)
                .ToSimplifiedBoardList();
            return Ok(boards);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var board = _unitOfWork.Board.GetOneIncludes(id, u => u.UserId == userId);
            if (board == null) return NotFound();
            return Ok(board.ToSimplifiedBoard());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBoardDto boardDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = _userManager.GetUserId(HttpContext.User);

            var boardModel = boardDto.ToBoard();

            boardModel.UserId = userId;

            await _unitOfWork.Board.AddAsync(boardModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = boardModel.BoardId }, boardModel.ToSimplifiedBoard());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBoardDto boardDto)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var boardModel = _unitOfWork.Board.GetOneIncludes(id, u => u.UserId == userId);
            if (boardModel == null) return NotFound();

            boardModel.Name = boardDto.Name;

            _unitOfWork.Board.Update(boardModel);
            await _unitOfWork.SaveAsync();
            return Ok(boardModel.ToSimplifiedBoard());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userId = _userManager.GetUserId(HttpContext.User);

            var boardModel = _unitOfWork.Board.GetOneIncludes(id, u => u.UserId == userId);

            if (boardModel == null) return NotFound();

            _unitOfWork.Board.Remove(boardModel);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
