using Microservice.Catalog.Api;
using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Courses;
using Microservice.Catalog.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Swagger ayarlar�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersioningExt();

// MassTransit-RabbitMQ Ayarlar�
builder.Services.AddMasstransitExt(builder.Configuration);

// Mongo ayarlar�
builder.Services.AddMongoOptionExt(); 
builder.Services.AddDatabaseServiceExt();


//Authentication ayarlar�
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);






var app = builder.Build();


var apiVersionSet = app.AddVersionSetExt();
app.AddCategoryGroupEndpointExt(apiVersionSet);


app.AddCourseGroupEndpointExt(apiVersionSet);

app.AddSeedDataExt().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message:"Seed data ekleme i�lemi tamamland�.");
});
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();

}


app.Run();
