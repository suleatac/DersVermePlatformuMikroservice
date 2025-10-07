using Microservice.Shared.Options;
using Microservice.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Shared.Extentions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services,IConfiguration configuration)
        {
            /* 
             
             4 parametreye bakacağız bunlar;
             Sign, 
             Aud => payment.api, 
             Iss => http://localhost:8080/realms/ExampleMikroservice, 
             Token lifetime 

            */

            var identityOption = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();

            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = identityOption.Adress;
                    options.RequireHttpsMetadata = false;
                    options.Audience = identityOption.Audience;

                    options.TokenValidationParameters=new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true, 
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RoleClaimType= "roles",
                        NameClaimType= "preferred_username"
                    };



                }).AddJwtBearer("ClientCredentialSchema", options =>
                {
                    options.Authority = identityOption.Adress;
                    options.RequireHttpsMetadata = false;
                    options.Audience = identityOption.Audience;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };



                });
            services.AddAuthorization(
                
                Options =>
            {
                Options.AddPolicy("ClientCredential", policy =>
                {
                    policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("client_id");
                });

                Options.AddPolicy("password", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);
                });
            }




                );
            return services;
        }
    }
}
