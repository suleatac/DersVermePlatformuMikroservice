using Microservice.Catalog.Api;
using Microservice.Catalog.Api.Features.Categories;
using Microservice.Catalog.Api.Options;
using Microservice.Catalog.Api.Repositories;
using Microservice.Shared.Extentions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Mongo ayarlarý
builder.Services.AddMongoOptionExt(); 

builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));


var app = builder.Build();
app.AddCategoryGroupEndpointExt();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
