using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synergy.Landing.DAL.Queries.Entities;

namespace Synergy.Landing.DAL.Queries.PostgreSQL.Configurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.TypeId).HasColumnName("TaskTypeId");
            builder.Property(x => x.StatusId).HasColumnName("TaskStatusId");
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Event).WithMany().HasForeignKey(x => x.EventId);
            builder.HasOne(x => x.Status).WithMany().HasForeignKey(x => x.StatusId);
            builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.TypeId);
        }
    }
}
