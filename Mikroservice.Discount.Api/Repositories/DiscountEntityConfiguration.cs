using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;


namespace Mikroservice.Discount.Api.Repositories
{
    public class DiscountEntityConfiguration : IEntityTypeConfiguration<Features.Discounts.Discount>
    {
        public void Configure(EntityTypeBuilder<Features.Discounts.Discount> builder)
        {

            builder.ToCollection("discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);
            builder.Property(x => x.Rate).HasElementName("rate");
      
            builder.Property(x => x.CreatedTime).HasElementName("created_time");
            builder.Property(x => x.UpdatedTime).HasElementName("updated_time");
            builder.Property(x => x.ExpiredTime).HasElementName("expired_time");
            builder.Property(x => x.UserId).HasElementName("user_id");
        }
    }
}
