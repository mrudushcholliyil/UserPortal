using Microsoft.EntityFrameworkCore;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Domain.Entities.Interfaces;
using UserPortal.Infrastructure.Persistence.DbContexts;

namespace UserPortal.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Generic repository implementation for common operation on entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid();

            await _dbSet.AddAsync(entity);            

            return entity;
        }

        public async Task<T> UpdateAsync(Guid id, T entity)
        {
            entity.Id = id;

            var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {id} not found.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);            

            return entity;
        }
    }
}
