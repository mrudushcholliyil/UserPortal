using UserPortal.Domain.Entities.Interfaces;

namespace UserPortal.Application.Common.Interfaces
{
    /// <summary>
    ///Generic repository interface for common operations on entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
    }
}