using Microservice.Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));



//Authentication ayarlarý
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);





var app = builder.Build();
app.UseExceptionHandler(x => { });
app.MapReverseProxy();
app.MapGet("/", () => "Yarp (Gateway)");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
