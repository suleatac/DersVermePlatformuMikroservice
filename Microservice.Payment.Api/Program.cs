using Microservice.Payment.Api;
using Microservice.Payment.Api.Features.Payment;
using Microservice.Payment.Api.Repositories;
using Microservice.Shared.Extentions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Swagger ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();


//InMemory Db ayarlarý
builder.Services.AddDbContext<AppDbContext>(options => { 
    options.UseInMemoryDatabase("Payment-In-MemoryDb");
});

var app = builder.Build();
app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}




app.Run();
