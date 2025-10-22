

using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Mikroservice.web.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mikroservice.web.Services
{
    public class TokenService(IHttpClientFactory httpClientFactory, IdentityOption identityOption)
    {

        public List<Claim> ExtractClaims(string accessToken)
        {

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            return jwtToken.Claims.ToList<Claim>();
        }
        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
            var authenticationProperties = new AuthenticationProperties {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
            };
            authenticationProperties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken!
                },
                new AuthenticationToken
                {
                    Name =  OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken!
                },
                new AuthenticationToken
                {
                    Name =  OpenIdConnectParameterNames.ExpiresIn,
                    Value = authenticationProperties.ExpiresUtc?.ToString("o")
                }




            });
            return authenticationProperties;
        }



        public async Task<TokenResponse> GetNewAccessTokenByRefreshToken(string refreshToken)
        {



            var discoveryRequest = new DiscoveryDocumentRequest() {
                Address = identityOption.Address,
                Policy =
               {
                    RequireHttps=false
                }
            };
            var httpClient = httpClientFactory.CreateClient("GetNewAccessTokenByRefreshToken");
            httpClient.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }


            var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                RefreshToken = refreshToken
            });

            return tokenResponse;


        }



        public async Task<TokenResponse> GetClientAccessToken()
        {
            var discoveryRequest = new DiscoveryDocumentRequest() {
                Address = identityOption.Address,
                Policy =
               {
                    RequireHttps=false
                }
            };
            var httpClient = httpClientFactory.CreateClient("GetClientAccessToken");
            httpClient.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync(discoveryRequest);
            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }


            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret
            });
            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }
            return tokenResponse;
        }
    }



}
