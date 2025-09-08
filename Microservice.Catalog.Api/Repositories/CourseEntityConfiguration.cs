using Microservice.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace Microservice.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration:IEntityTypeConfiguration<Course>
    {

        public void Configure(EntityTypeBuilder<Course> builder)
        {



      
            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.Price).HasElementName("price");
            builder.Property(x => x.UserId).HasElementName("user_id");
            builder.Property(x => x.Picture).HasElementName("picture");
            builder.Property(x => x.CreatedDate).HasElementName("created_date");
            builder.Property(x => x.CategoryId).HasElementName("category_id");
            builder.Ignore(c => c.Category);
            builder.OwnsOne(c => c.Feature,
                feature =>
                {
                    feature.HasElementName("feature");
                    feature.Property(x => x.Duration).HasElementName("duration");
                    feature.Property(x => x.Rating).HasElementName("rating");
                    feature.Property(x => x.EducatorFullName).HasElementName("educator_full_name").HasMaxLength(100);
                }
            );
        }




    }
}
