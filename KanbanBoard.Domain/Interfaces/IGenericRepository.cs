using KanbanBoard.Domain.Entities;
using System.Linq.Expressions;

namespace KanbanBoard.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T>? Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }

}
