using UserPortal.Application.Common.Interfaces;
using UserPortal.Domain.Entities;
using UserPortal.Infrastructure.Persistence.DbContexts;

namespace UserPortal.Infrastructure.Persistence.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        // We can implement additional methods specific to UserEntity here if needed
    }
}
