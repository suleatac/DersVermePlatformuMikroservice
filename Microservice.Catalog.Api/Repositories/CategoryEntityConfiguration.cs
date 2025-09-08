using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace Microservice.Catalog.Api.Repositories
{
    public class CategoryEntityConfiguration: IEntityTypeConfiguration<Category>
    {


        public void Configure(EntityTypeBuilder<Category> builder)
        {


            builder.ToCollection("categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(c => c.Courses);


        }



    }
}
