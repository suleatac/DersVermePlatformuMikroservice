using Microservice.Bus;
using Microservice.Payment.Api;
using Microservice.Payment.Api.Features.Payment;
using Microservice.Payment.Api.Repositories;
using Microservice.Shared.Extentions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Swagger ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddVersioningExt();

// MassTransit-RabbitMQ Ayarları
builder.Services.AddCommonMasstransitExt(builder.Configuration);




//Authentication ayarları
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);



//InMemory Db ayarları
builder.Services.AddDbContext<AppDbContext>(options => { 
    options.UseInMemoryDatabase("Payment-In-MemoryDb");
});




var app = builder.Build();

app.MapDefaultEndpoints();
app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();


app.Run();
