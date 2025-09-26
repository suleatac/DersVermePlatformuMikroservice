using Microservice.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Order.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           builder.HasKey(oi => oi.Id);
              builder.Property(oi => oi.Id).UseIdentityColumn();
                builder.Property(oi => oi.ProductId).IsRequired();
                builder.Property(oi => oi.ProductName).IsRequired().HasMaxLength(200);
                builder.Property(oi => oi.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
                builder.HasOne(oi => oi.Order)
                       .WithMany(o => o.OrderItems)
                       .HasForeignKey(oi => oi.OrderId)
                       .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
