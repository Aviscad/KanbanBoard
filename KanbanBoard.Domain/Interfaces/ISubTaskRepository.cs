﻿using KanbanBoard.Domain.Entities;

namespace KanbanBoard.Domain.Interfaces
{
    public interface ISubTaskRepository : IGenericRepository<SubTask>
    {
        IEnumerable<SubTask> GetAllIncludes();
        SubTask? GetOneIncludes(int id);
    }
}
