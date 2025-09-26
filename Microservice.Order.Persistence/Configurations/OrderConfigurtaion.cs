using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Persistence.Configurations
{
    public class OrderConfigurtaion : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.Property(o => o.Code).IsRequired().HasMaxLength(10);
            builder.Property(o => o.BuyerId).IsRequired();
            builder.Property(o => o.Status).IsRequired().HasMaxLength(50);
            builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
      
            builder.Property(o => o.Created).IsRequired();
            builder.Property(o => o.AddressId).IsRequired();
            builder.Property(o => o.DiscountRate).IsRequired(false).HasColumnType("float");
            builder.HasMany(o => o.OrderItems)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Address)
                   .WithMany()
                   .HasForeignKey(o => o.AddressId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
