using DataAccess.Entities;
using DataAccess.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class ActivityContext : IdentityDbContext<User, Role, Guid>
    {
        public ActivityContext(DbContextOptions<ActivityContext> options)
            : base(options)
        {

        }

        public DbSet<Activity> Activites { get; set; }
        public DbSet<Error> Errors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ActivityEntityConfiguration());
            builder.ApplyConfiguration(new ErrorEntityConfiguration());
            builder.ApplyConfiguration(new UserActivityEntityTypeConfiguration());

            base.OnModelCreating(builder);
            //neden böyle?
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
