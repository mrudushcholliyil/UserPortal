using Microsoft.EntityFrameworkCore;
using UserPortal.Domain.Entities;

namespace UserPortal.Infrastructure.Persistence.DbContexts
{
    /// <summary>
    /// DbContext for the User Portal application, managing the database connection and entity sets.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<UserEntity> Users { get; set; }
    
    }
}
