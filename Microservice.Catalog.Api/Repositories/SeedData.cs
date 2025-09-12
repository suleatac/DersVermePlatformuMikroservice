using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Courses;
using MongoDB.Driver;

namespace Microservice.Catalog.Api.Repositories
{
    public static class SeedData
    {

        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();


            dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;










            if (!dbContext.Categories.Any())
            {
                var categories = new List<Category>()
                {
                 new() {Id=NewId.NextSequentialGuid(),Name="Development"},
                 new() {Id=NewId.NextSequentialGuid(),Name="Business"},
                 new() {Id=NewId.NextSequentialGuid(),Name="IT & Software"},
                 new() {Id=NewId.NextSequentialGuid(),Name="Office Productivity"},
                 new() {Id=NewId.NextSequentialGuid(),Name="Personal Development"},
                };
                dbContext.Categories.AddRange(categories);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Courses.Any())
            {
                var category=await dbContext.Categories.FirstAsync();
                var randomUserId = NewId.NextSequentialGuid();
                List<Course> courses = new()
                {
                    new()
                    {
                        Id=NewId.NextSequentialGuid(),
                        Name="Complete C# Developer Course",
                        Description="Learn C# from scratch to advanced level with practical examples and projects.",
                        Price=99.99M,
                        UserId=randomUserId,
                        CategoryId=category.Id,
                        CreatedDate=DateTime.UtcNow
                    },
                    new()
                    {
                        Id=NewId.NextSequentialGuid(),
                        Name="ASP.NET Core Web API",
                        Description="Build robust and scalable Web APIs using ASP.NET Core framework.",
                        Price=89.99M,
                        UserId=randomUserId,
                        CategoryId=category.Id,
                        CreatedDate=DateTime.UtcNow
                    },
                    new()
                    {
                        Id=NewId.NextSequentialGuid(),
                        Name="Entity Framework Core",
                        Description="Master Entity Framework Core for data access in .NET applications.",
                        Price=79.99M,
                        UserId=randomUserId,
                        CategoryId=category.Id,
                        CreatedDate=DateTime.UtcNow
                    }
                };

                dbContext.Courses.AddRange(courses);
                await dbContext.SaveChangesAsync();




            }
        }



    }
}
