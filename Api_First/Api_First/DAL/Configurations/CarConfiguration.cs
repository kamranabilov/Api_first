using Api_First.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.DAL.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(c => c.Brand).HasMaxLength(30).IsRequired();
            builder.Property(c => c.Model).HasMaxLength(30).IsRequired();
            builder.Property(c => c.Price).HasColumnType("decimal(9,2)").IsRequired();
            builder.Property(c => c.Color).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Display).HasDefaultValue(true);
        }
    }
}
