﻿
namespace Microservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           var existCategory= await context.Categories
                .AnyAsync(x => x.Name == request.Name, cancellationToken:cancellationToken);


            if (existCategory)
            {
                ServiceResult<CreateCategoryResponse>.Error("Category Name already exist", $"The category name'{request.Name}'",HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };
            await context.AddAsync(category, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"<empty>");


        }
    }
}
