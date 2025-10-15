using Microservice.web.Services;
using Microservice.web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Mikroservice.web.ExceptionHandlers;
using Mikroservice.web.Extentions;
using Mikroservice.web.Pages.Auth.SignIn;
using Mikroservice.web.Pages.Auth.SignUp;
using Mikroservice.web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOptionsExt();
builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();

builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRefitConfigurationExt(builder.Configuration);



//Cookie Authentication Ayarlarý
builder.Services.AddAuthentication(configureOption => {
    configureOption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOption.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options => {
        options.LoginPath = "/Auth/SignIn";
        options.LogoutPath = "/Auth/SignOut";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.Name = "MikroserviceAuthWebCookie";
        options.AccessDeniedPath = "/Auth/AccessDenied";
      
    });

builder.Services.AddAuthorization();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
