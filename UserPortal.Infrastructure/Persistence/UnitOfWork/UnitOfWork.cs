using UserPortal.Application.Common.Interfaces;
using UserPortal.Infrastructure.Persistence.DbContexts;

namespace UserPortal.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork(ApplicationDbContext DbContext) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
