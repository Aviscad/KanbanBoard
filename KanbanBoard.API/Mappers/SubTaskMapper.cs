using KanbanBoard.API.Models.SubTasks;
using KanbanBoard.API.Models.Tasks;
using KanbanBoard.Domain.Entities;

namespace KanbanBoard.API.Mappers
{
    public static class SubTaskMapper
    {
        public static SubTaskDto ToSubTaskDto(this SubTask subTask) 
        {
            return new SubTaskDto
            {
                SubTaskId = subTask.SubTaskId,
                Name = subTask.Name,
                Completed = subTask.Completed,
                TaskId = subTask.TaskId
            };
        }

        public static SubTask ToSubTask(this SubTaskDto subTask)
        {
            return new SubTask
            {
               SubTaskId = subTask.SubTaskId,
               Name = subTask.Name,
               Completed = subTask.Completed,
            };
        }

        public static SubTask ToTask(this CreateSubTaskDto subTask)
        {
            return new SubTask
            {
                Name = subTask.Name,
                Completed = subTask.Completed,
                TaskId = subTask.TaskId,    
            };
        }

        public static SubTask ToTask(this UpdateSubTaskDto subTask)
        {
            return new SubTask
            {
                Name = subTask.Name,
                Completed = subTask.Completed,
            };
        }
    }
}
