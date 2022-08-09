using Api_First.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DAL.Configurations
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(e => e.HP).IsRequired();
            builder.Property(e => e.Value).HasMaxLength(5).IsRequired();
            builder.Property(e => e.Torque).IsRequired();
        }
    }
}
