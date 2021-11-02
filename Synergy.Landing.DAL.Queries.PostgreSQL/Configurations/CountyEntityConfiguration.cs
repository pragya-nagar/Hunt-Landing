using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synergy.Landing.DAL.Queries.Entities;

namespace Synergy.Landing.DAL.Queries.PostgreSQL.Configurations
{
    public class CountyEntityConfiguration : IEntityTypeConfiguration<County>
    {
        public void Configure(EntityTypeBuilder<County> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name);
            builder.HasMany(x => x.Events);
        }
    }
}
