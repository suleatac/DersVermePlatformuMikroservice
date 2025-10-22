using Microservice.Basket.Api;
using Microservice.Basket.Api.Features.Baskets;
using Microservice.Bus;
using Microservice.Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Swagger ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCommonServiceExt(typeof(BasketAssembly));

// MassTransit-RabbitMQ Ayarlarý
builder.Services.AddMasstransitExt(builder.Configuration);

// Version Ayarlarý
builder.Services.AddVersioningExt();




builder.Services.AddScoped<BasketService>();

// Redis Baðlantý ayarlarý
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//Authentication ayarlarý
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();
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
