using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Columns;
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
    public class ColumnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public ColumnController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var columns = _unitOfWork.Column
                .GetAllIncludes(c => c.Board.UserId == _userService.GetId(User))
                .ToSimplifiedColumnList();

            return Ok(columns);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var column = _unitOfWork.Column
                .GetOneIncludes(id, c => c.Board.UserId == _userService.GetId(User));

            return column == null ? NotFound() : Ok(column.ToSimplifiedColumn());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateColumnDto columnDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownsBoard = _unitOfWork.Board.Find(b => b.BoardId == columnDto.BoardId && b.UserId == _userService.GetId(User));

            if (ownsBoard == null || !ownsBoard.Any()) return Unauthorized("The user doesn't have access to Board");

            var board = _unitOfWork.Board.Find(b => b.UserId == _userService.GetId(User));

            if (board == null) return BadRequest();

            var columnModel = columnDto.ToColumn();
            await _unitOfWork.Column.AddAsync(columnModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = columnModel.ColumnId }, columnModel.ToSimplifiedColumn());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateColumnDto columnDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var columnModel = _unitOfWork.Column
                .GetOneIncludes(id, c => c.Board.UserId == _userService.GetId(User));

            if (columnModel == null) return BadRequest();

            columnModel.Name = columnDto.Name;

            _unitOfWork.Column.Update(columnModel);
            await _unitOfWork.SaveAsync();
            return Ok(columnModel.ToSimplifiedColumn());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var columnModel = _unitOfWork.Column
                 .GetOneIncludes(id, c => c.Board.UserId == _userService.GetId(User));

            if (columnModel == null) return NotFound();

            _unitOfWork.Column.Remove(columnModel);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}
