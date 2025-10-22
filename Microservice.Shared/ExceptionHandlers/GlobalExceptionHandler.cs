using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Shared.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
           
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails {
                Title = "An unexpected error occurred.",
                Type = exception.GetType().Name,
                Status = (int)HttpStatusCode.InternalServerError
            }, cancellationToken);

            return true;
        }
    }
}
