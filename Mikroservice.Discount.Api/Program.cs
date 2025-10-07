using Asp.Versioning.Builder;
using Microservice.Bus;
using Mikroservice.Discount.Api;
using Mikroservice.Discount.Api.Features.Discounts;
using Mikroservice.Discount.Api.Options;
using Mikroservice.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();




// Swagger ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddVersioningExt();

// MassTransit-RabbitMQ Ayarlarý
builder.Services.AddCommonMasstransitExt(builder.Configuration);



// Mongo ayarlarý
builder.Services.AddMongoOptionExt();
builder.Services.AddDatabaseServiceExt();

//Authentication ayarlarý
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);




var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();





app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}



app.Run();

