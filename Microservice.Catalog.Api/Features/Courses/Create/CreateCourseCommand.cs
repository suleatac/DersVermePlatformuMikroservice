namespace Microservice.Catalog.Api.Features.Courses.Create
{
    //public record CreateCourseCommand(
    //    string Name,
    //    string Description,
    //    decimal Price,
    //    IFormFile? Picture,
    //    Guid CategoryId) : IRequestByServiceResult<Guid>;



    public record CreateCourseCommand : IRequestByServiceResult<Guid>
    {
        public string Name { get; init; }=null!;
        public string Description { get; init; }=null!;
        public decimal Price { get; init; }
        public IFormFile? Picture { get; init; }
        public Guid CategoryId { get; init; }

        
    }
}
