using Microservice.Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();



builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));



//Authentication ayarları
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);





var app = builder.Build();

app.MapDefaultEndpoints();
app.UseExceptionHandler(x => { });
app.MapReverseProxy();
app.MapGet("/", () => "Yarp (Gateway)");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
