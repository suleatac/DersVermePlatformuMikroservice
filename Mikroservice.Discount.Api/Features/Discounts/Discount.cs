using Mikroservice.Discount.Api.Repositories;

namespace Mikroservice.Discount.Api.Features.Discounts
{
    public class Discount:BaseEntity
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public float Rate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
