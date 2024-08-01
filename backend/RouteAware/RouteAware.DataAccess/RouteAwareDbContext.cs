using Microsoft.EntityFrameworkCore;
using RouteAware.DataAccess.Entities;

namespace RouteAware.DataAccess
{
    public class RouteAwareDbContext : DbContext
    {
        public RouteAwareDbContext(DbContextOptions<RouteAwareDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<AccidentEntity> Accidents { get; set; }
        public DbSet<RouteAwareUserEntity> Users { get; set; }
    }
}
