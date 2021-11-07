using DataAccess.Entities;
using DataAccess.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ActivityContext : IdentityDbContext<User>
    {
        public DbSet<Activity> Activites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Activity>().ToTable("Activites");
            builder.ApplyConfiguration(new ActivityEntityConfiguration());
        }
    }
}
