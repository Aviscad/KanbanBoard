using KanbanBoard.API.Models.Boards;
using KanbanBoard.API.Models.Columns;
using KanbanBoard.API.Models.Tasks;
using KanbanBoard.Domain.Entities;
using Task = KanbanBoard.Domain.Entities.Task;

namespace KanbanBoard.API.Mappers
{
    public static class TaskMapper
    {
        public static TaskDto ToTaskDto(this Task task)
        {
            return new TaskDto
            {
               ColumnId = task.ColumnId,
               Description = task.Description,
               Title = task.Title,
               SubTasks = task.SubTasks
            };
        }
        public static SimplifiedTask ToSimplifiedTask(this Task task)
        {
            return new SimplifiedTask
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                ColumnId = task.ColumnId,
                ColumnName = task.Column.Name, 
                SubTasks = task.SubTasks.ToList()
                
            };
        }

        public static List<SimplifiedTask> ToSimplifiedTaskList(this IEnumerable<Task> tasks)
        {
            return tasks.Select(task => new SimplifiedTask
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                ColumnId = task.ColumnId,
                ColumnName = task.Column.Name,
                SubTasks = task.SubTasks.ToList()

            }).ToList();
        }

        public static Task ToTask(this CreateTaskDto createTaskDto)
        {
            return new Task
            {
                ColumnId=createTaskDto.ColumnId,
                Description = createTaskDto.Description,
                Title = createTaskDto.Title,
            };
        }
    }
}
