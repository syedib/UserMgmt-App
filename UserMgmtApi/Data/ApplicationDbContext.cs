using Microsoft.EntityFrameworkCore;
using UserMgmtApi.Data.Config;
using UserMgmtApi.Models;

namespace UserMgmtApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserDetailConfiguration());
        }
    }
}
