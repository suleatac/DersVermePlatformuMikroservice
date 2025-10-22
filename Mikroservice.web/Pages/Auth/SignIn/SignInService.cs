using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Mikroservice.web.Options;
using Mikroservice.web.Services;
using System.Security.Claims;

namespace Mikroservice.web.Pages.Auth.SignIn
{








    public class SignInService(IHttpContextAccessor httpContextAccessor,TokenService tokenService,IdentityOption identityOption, HttpClient client, ILogger<SignInService> logger)
    {

        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {
            var tokenResponse = await GetAccessToken(signInViewModel);
            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                return ServiceResult.Error(tokenResponse!.Error!,tokenResponse.ErrorDescription!);
            }

            //Burası önemli cookie oluşturuyoruz.
            var userClaims = tokenService.ExtractClaims(tokenResponse.AccessToken!);
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
 
            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);


            return ServiceResult.Success();
        }

        private async Task<TokenResponse> GetAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest() {
                Address = identityOption.Address,
                Policy =
                {
                    RequireHttps=false
                }
            };

            client.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }



            var tokenResponse = await client.RequestPasswordTokenAsync( new PasswordTokenRequest {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email!,
                Password = signInViewModel.Password,
                Scope = "offline_access"
            });





            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }


            return tokenResponse;
        }


    }
}
