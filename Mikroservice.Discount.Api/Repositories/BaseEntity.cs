using MongoDB.Bson.Serialization.Attributes;

namespace Mikroservice.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
