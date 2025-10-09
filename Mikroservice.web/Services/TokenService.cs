

using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mikroservice.web.Services
{
    public class TokenService
    {

        public List<Claim> ExtractClaims(string accessToken)
        {

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            return jwtToken.Claims.ToList<Claim>();
        }



        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
     var authenticationProperties = new AuthenticationProperties
            {
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








    }



}
