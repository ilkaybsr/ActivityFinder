using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations
{
    public class ActivityEntityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Location);

            builder.Property(x => x.Address);

            builder.Property(x => x.Category);

            builder.Property(x => x.Description);

        }
    }
}
