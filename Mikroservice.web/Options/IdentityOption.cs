namespace Mikroservice.web.Options
{
    public class IdentityOption
    {
        public required string Address { get; set; }
        public required string BaseAddress { get; set; }
        public required IdentityOptionItems Admin { get; set; } = null!;
        public required IdentityOptionItems Web { get; set; }=null!;
    }
    public class IdentityOptionItems
    {

        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}
