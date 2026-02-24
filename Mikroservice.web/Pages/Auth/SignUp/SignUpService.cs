using Duende.IdentityModel.Client;
using Mikroservice.web.Options;
using Mikroservice.web.Services;

namespace Mikroservice.web.Pages.Auth.SignUp
{
    public record UserCreateRequest(string Username,
        bool Enabled,
        string FirstName,
        string LastName,
        string Email,
        List<Credential> Credentials);

    public record Credential(
        string Type,
        string Value,
        bool Temporary = false
        );

    public record KeycloakErrorResponse(string ErrorMessage);
    public class SignUpService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
    {
     
        public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
        {
            var token = await GetClientCredentialTokenAsAdmin();
            var adress = $"{identityOption.BaseAddress}/admin/realms/ExampleMikroservice/users";
            client.SetBearerToken(token);
            var userCreateRequest = CreateUserCreateRequest(model);
            var response = await client.PostAsJsonAsync(adress, userCreateRequest);
            if (!response.IsSuccessStatusCode)
            { 
                if(response.StatusCode!=System.Net.HttpStatusCode.InternalServerError)
                {
                    var keycloakErrorResponse = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                    return ServiceResult.Error("System error occurred. {error}", keycloakErrorResponse!.ErrorMessage);
                }








                var error = await response.Content.ReadAsStringAsync();
                logger.LogError(error);   
                return ServiceResult.Error("System error occurred. Please try again later.");
            }

            return ServiceResult.Success();
        }
        
        private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
        {
            return new UserCreateRequest(
                model.UserName!,
                true,
                model.FirstName!,
                model.LastName!,
                model.Email!,
                new List<Credential>()
                {
                    new Credential("password",model.Password!,false)
                });
        }
        private async Task<string> GetClientCredentialTokenAsAdmin()
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
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }
         

            return tokenResponse.AccessToken!;
        }
    
   
    }
}
