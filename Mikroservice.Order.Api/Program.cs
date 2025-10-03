using Microservice.Order.Application;
using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Application.Contracts.UnitOfWorks;
using Microservice.Order.Persistence;
using Microservice.Order.Persistence.Repositories;
using Microservice.Order.Persistence.UnitOfWork;
using Microservice.Shared.Extentions;
using Microsoft.EntityFrameworkCore;
using Mikroservice.Order.Api.Endpoints.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Version ayarlarý
builder.Services.AddVersioningExt();

builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(OrderAssembly)));

//Authentication ayarlarý
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);




builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Swagger ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));



//Migration settings
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

});










var app = builder.Build();

var apiVersionSet = app.AddVersionSetExt();
app.AddOrderGroupEndpointExt(apiVersionSet);



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
