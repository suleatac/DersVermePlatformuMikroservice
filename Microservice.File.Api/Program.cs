using Microservice.Bus;
using Microservice.File.Api;
using Microservice.File.Api.Features.File;
using Microservice.Shared.Extentions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



// Swagger ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//file kök dosyasýna eriþim için
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersioningExt();

// MassTransit-RabbitMQ Ayarlarý
builder.Services.AddMasstransitExt(builder.Configuration);



//Authentication ayarlarý
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);









var app = builder.Build();
app.AddFileGroupEndpointExt(app.AddVersionSetExt());
app.UseStaticFiles();





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

