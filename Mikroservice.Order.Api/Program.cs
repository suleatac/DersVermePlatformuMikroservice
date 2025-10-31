using Microservice.Bus;
using Microservice.Order.Application;
using Microservice.Order.Application.BackgroundServices;
using Microservice.Order.Application.Contracts.Refit;
using Microservice.Order.Application.Contracts.Refit.PaymentService;
using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Application.Contracts.UnitOfWorks;
using Microservice.Order.Persistence;
using Microservice.Order.Persistence.Repositories;
using Microservice.Order.Persistence.UnitOfWork;
using Microservice.Shared.Extentions;
using Microservice.Shared.Options;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mikroservice.Order.Api.Endpoints.Orders;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Version ayarları
builder.Services.AddVersioningExt();

builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(OrderAssembly)));


// MassTransit-RabbitMQ Ayarları
builder.Services.AddCommonMasstransitExt(builder.Configuration);




//Authentication ayarları
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);




builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Swagger ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));



//Migration settings
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

//});
// Aspire'ın ürettiği bağlantı adını kullanıyoruz: "order-db-aspire"
var conn = builder.Configuration.GetConnectionString("order-db-aspire");

// Manuel AddDbContext ile TrustServerCertificate/Encrypt ve retry ekliyoruz
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(
        $"{conn};TrustServerCertificate=True;Encrypt=False",
        sql => sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null)
    );
});

builder.Services.AddRefitConfigurationExt(builder.Configuration);

builder.Services.AddHostedService<CheckPaymentStatusOrderBackgroundService>();


var app = builder.Build();

app.MapDefaultEndpoints();

// Otomatik migration: retry + log (DB hazır olana kadar bekle)
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    var logger = sp.GetRequiredService<ILogger<Program>>();
    var db = sp.GetRequiredService<AppDbContext>();

    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);

    for (var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            await db.Database.MigrateAsync();
            logger.LogInformation("Database migrated successfully.");
            break;
        }
        catch (SqlException ex)
        {
            logger.LogWarning(ex, "SQL Server not ready (attempt {Attempt}/{Max}). Waiting {Delay}s...", attempt, maxRetries, delay.TotalSeconds);
            if (attempt == maxRetries) throw;
            await Task.Delay(delay);
            delay = TimeSpan.FromSeconds(Math.Min(delay.TotalSeconds * 2, 60));
        }
    }
}



//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
//    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

//    var maxRetries = 10;
//    var delay = TimeSpan.FromSeconds(5);

//    for (var attempt = 1; attempt <= maxRetries; attempt++)
//    {
//        try
//        {
//            await dbContext.Database.MigrateAsync();
//            logger.LogInformation("Database migrated successfully.");
//            break;
//        }
//        catch (SqlException ex)
//        {
//            logger.LogWarning(ex, "SQL Server not ready (attempt {Attempt}/{Max}). Waiting {Delay}s...", attempt, maxRetries, delay.TotalSeconds);
//            if (attempt == maxRetries) throw;
//            await Task.Delay(delay);
//        }
//    }
//}




app.UseExceptionHandler(x => { });
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
