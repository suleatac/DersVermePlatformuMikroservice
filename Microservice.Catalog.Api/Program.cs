using Microservice.Catalog.Api;
using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Features.Courses;
using Microservice.Catalog.Api.Options;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Swagger ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersioningExt();

// MassTransit-RabbitMQ Ayarları
builder.Services.AddMasstransitExt(builder.Configuration);

// Mongo ayarları
builder.Services.AddMongoOptionExt(); 
builder.Services.AddDatabaseServiceExt();


//Authentication ayarları
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);






var app = builder.Build();

app.MapDefaultEndpoints();
app.UseExceptionHandler(x => { });

var apiVersionSet = app.AddVersionSetExt();
app.AddCategoryGroupEndpointExt(apiVersionSet);


app.AddCourseGroupEndpointExt(apiVersionSet);

app.AddSeedDataExt().ContinueWith(x =>
{
    Console.WriteLine(x.IsFaulted ? x.Exception?.Message:"Seed data ekleme işlemi tamamlandı.");
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
