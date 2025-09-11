using AutoMapper;
using Microservice.Catalog.Api.Features.Categories.Dtos;

namespace Microservice.Catalog.Api.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
