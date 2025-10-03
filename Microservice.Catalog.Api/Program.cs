using Microservice.Catalog.Api;
using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Courses;
using Microservice.Catalog.Api.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddVersioningExt();
// Mongo ayarlarý
builder.Services.AddMongoOptionExt(); 

builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
//Authentication ayarlarý
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();


var apiVersionSet = app.AddVersionSetExt();
app.AddCategoryGroupEndpointExt(apiVersionSet);


app.AddCourseGroupEndpointExt(apiVersionSet);

app.AddSeedDataExt().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message:"Seed data ekleme iþlemi tamamlandý.");
});
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
