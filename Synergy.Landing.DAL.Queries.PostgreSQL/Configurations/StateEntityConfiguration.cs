﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synergy.Landing.DAL.Queries.Entities;

namespace Synergy.Landing.DAL.Queries.PostgreSQL.Configurations
{
    public class StateEntityConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name);
            builder.Property(x => x.Abbreviation);
        }
    }
}
