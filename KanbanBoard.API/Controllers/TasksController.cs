using KanbanBoard.API.Mappers;
using KanbanBoard.API.Models.Boards;
using KanbanBoard.API.Models.Columns;
using KanbanBoard.API.Models.Tasks;
using KanbanBoard.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TasksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _unitOfWork.Task
                .GetAllIncludes()
                .ToSimplifiedTaskList();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var task = _unitOfWork.Task.GetOneIncludes(id);

            if (task == null) return NotFound();
            return Ok(task.ToSimplifiedTask());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto taskDto)
        {
            if (taskDto == null) return BadRequest();

            var taskModel = taskDto.ToTask();
            await _unitOfWork.Task.AddAsync(taskModel);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = taskModel.TaskId }, taskModel.ToTaskDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskDto taskDto)
        {
            var taskModel = await _unitOfWork.Task.GetByIdAsync(id);
            if (taskModel == null) return NotFound();

            taskModel.Title = taskDto.Title;
            taskModel.Description = taskDto.Description;
            taskModel.ColumnId = taskDto.ColumnId;

            _unitOfWork.Task.Update(taskModel);
            await _unitOfWork.SaveAsync();

            return Ok(taskModel.ToTaskDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var taskModel = await _unitOfWork.Task.GetByIdAsync(id);
            if (taskModel == null) return NotFound();

            _unitOfWork.Task.Remove(taskModel);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
