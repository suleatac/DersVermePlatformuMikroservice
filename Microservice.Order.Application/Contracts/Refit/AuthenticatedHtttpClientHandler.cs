using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Contracts.Refit
{
    public class AuthenticatedHtttpClientHandler(IHttpContextAccessor httpContextAccessor):DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //eğer null ise demekki bi request gelmemiş demektir. bu gelen backgroundanmış.
            if (httpContextAccessor.HttpContext == null)
            {
                return await base.SendAsync(request, cancellationToken);
            }


            if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            string? token = null;
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                token = authorizationHeader.ToString().Split(" ")[1];
            }

            if(!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
       
    }
}
