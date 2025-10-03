using Microservice.Basket.Api;
using Microservice.Basket.Api.Features.Baskets;
using Microservice.Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Swagger ayarlar�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(BasketAssembly));

// Version Ayarlar�
builder.Services.AddVersioningExt();




builder.Services.AddScoped<BasketService>();

// Redis Ba�lant� ayarlar�
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

//Authentication ayarlar�
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();




app.Run();
