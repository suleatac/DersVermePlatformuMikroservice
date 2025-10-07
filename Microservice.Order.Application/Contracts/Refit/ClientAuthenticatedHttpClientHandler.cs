using Duende.IdentityModel.Client;
using Microservice.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Contracts.Refit
{
    public class ClientAuthenticatedHttpClientHandler(IServiceProvider serviceProvider,IHttpClientFactory httpClientFactory ) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (request.Headers.Authorization is not null)
            {
                return await base.SendAsync(request, cancellationToken);
            }


            using var scope = serviceProvider.CreateScope();
            var identityOptions = scope.ServiceProvider.GetRequiredService<IdentityOption>();


            var ClientSecretOptions = scope.ServiceProvider.GetRequiredService<ClientSecretOption>();




            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOptions.Adress,
                Policy = { RequireHttps = false }
            };

            var client = httpClientFactory.CreateClient();
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest, cancellationToken);

            if (discoveryResponse.IsError)
            {
                throw new Exception(discoveryResponse.Error);
            }
            var tokenRequest = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = ClientSecretOptions.Id,
                ClientSecret = ClientSecretOptions.Secret,
                Scope = identityOptions.Audience
            }, cancellationToken);

            if (tokenRequest.IsError)
            {
                throw new Exception(tokenRequest.Error);
            }


            request.SetBearerToken(tokenRequest.AccessToken!);
            return await base.SendAsync(request, cancellationToken);

        }
    }
}