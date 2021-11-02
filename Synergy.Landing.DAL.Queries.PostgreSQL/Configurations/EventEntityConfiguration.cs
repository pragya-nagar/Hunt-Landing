using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synergy.Landing.DAL.Queries.Entities;

namespace Synergy.Landing.DAL.Queries.PostgreSQL.Configurations
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Number).HasColumnName("EventNumber");
            builder.Property(x => x.TypeId).HasColumnName("EventTypeId");
            builder.Property(x => x.StateId);
            builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.TypeId);
            builder.HasOne(x => x.State).WithMany().HasForeignKey(x => x.StateId);
            builder.HasOne(x => x.County).WithMany().HasForeignKey(x => x.CountyId);
        }
    }
}
