﻿using Microsoft.AspNetCore.Diagnostics;

namespace Mikroservice.web.ExceptionHandlers
{
    public class UnauthorizedAccessExceptionHandler:IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is UnauthorizedAccessException)
            {
                context.Response.Redirect("/Auth/SignIn");
                return ValueTask.FromResult(true);
            }
            return ValueTask.FromResult(false);
        }
    }
}
