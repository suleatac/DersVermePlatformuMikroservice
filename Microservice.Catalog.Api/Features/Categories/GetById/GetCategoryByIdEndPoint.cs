//using AutoMapper;

namespace Microservice.Catalog.Api.Features.Categories.GetById
{


    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
   

    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) :
        IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {

            var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);




            if (hasCategory == null)
            {
                return ServiceResult<CategoryDto>.Error("Category not found",
                    $"The Category with id({request.Id}) not found.", HttpStatusCode.NotFound);
            }



            var categoryAsDto = mapper.Map<CategoryDto>(hasCategory);
            return ServiceResult<CategoryDto>.SuccessAsOK(categoryAsDto);
        }
    }




    public static class GetCategoryByIdEndPoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
                    (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult())
                      .MapToApiVersion(1.0)
                .WithName("GetByIdCategory");
            return group;
        }
      
    }
}
