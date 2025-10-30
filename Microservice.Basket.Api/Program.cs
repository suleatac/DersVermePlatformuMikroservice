using Microservice.Basket.Api;
using Microservice.Basket.Api.Features.Baskets;
using Microservice.Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Swagger ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCommonServiceExt(typeof(BasketAssembly));

// MassTransit-RabbitMQ Ayarları
builder.Services.AddMasstransitExt(builder.Configuration);

// Version Ayarları
builder.Services.AddVersioningExt();




builder.Services.AddScoped<BasketService>();

// Redis Bağlantı ayarları

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//});
builder.AddRedisDistributedCache("redis-db-basket");




//Authentication ayarları
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(x => { });

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}






app.Run();
