using DataAccess.Entities;
using DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ActivityContext : DbContext
    {
        public DbSet<Activity> Activites { get; set; }


        public ActivityContext(DbContextOptions<ActivityContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ActivityEntityConfiguration());
        }
    }
}
