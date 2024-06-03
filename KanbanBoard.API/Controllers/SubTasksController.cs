using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Columns;
using KanbanBoard.API.Models.SubTasks;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubTasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubTasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subtasks = _unitOfWork.SubTask
                .GetAllIncludes()
                .ToSimplifiedSubTaskDto();
            return Ok(subtasks);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var subtaskModel = _unitOfWork.SubTask.GetOneIncludes(id);
            if (subtaskModel == null) return NotFound();

            return Ok(subtaskModel.ToSubTaskDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubTaskDto subTask)
        {
            var subtaskModel = subTask.ToTask();
            await _unitOfWork.SubTask.AddAsync(subtaskModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = subtaskModel.SubTaskId }, subtaskModel.ToSubTaskDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubTaskDto subTaskDto)
        {
            var subtaskModel = await _unitOfWork.SubTask.GetByIdAsync(id);
            if (subtaskModel == null) return NotFound();

            subtaskModel.Name = subTaskDto.Name;
            subtaskModel.Completed = subTaskDto.Completed;
            subtaskModel.TaskId = subTaskDto.TaskId;

            _unitOfWork.SubTask.Update(subtaskModel);
            await _unitOfWork.SaveAsync();

            return Ok(subtaskModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subtaskModel = await _unitOfWork.SubTask.GetByIdAsync(id);
            if (subtaskModel == null) return NotFound();

            _unitOfWork.SubTask.Remove(subtaskModel);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
