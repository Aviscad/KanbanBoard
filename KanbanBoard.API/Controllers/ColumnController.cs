using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Columns;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColumnController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var columns = await _unitOfWork.Column.GetAllAsync();
            return Ok(columns);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id) {
            var column = await _unitOfWork.Column.GetByIdAsync(id);
            return column == null ? NotFound() : Ok(column);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateColumnDto columnDto)
        { 
            if (columnDto == null) return BadRequest();
            var columnModel = columnDto.ToColumn();
            await _unitOfWork.Column.AddAsync(columnModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = columnModel.ColumnId }, columnModel.ToColumnDto());
        }

    }
}
