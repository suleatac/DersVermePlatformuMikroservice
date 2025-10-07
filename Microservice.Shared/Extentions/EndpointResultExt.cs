﻿using Microsoft.AspNetCore.Http;
using HttpStatusCode = System.Net.HttpStatusCode;
namespace Microservice.Shared.Extentions
{
    public static class EndpointResultExt
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> result)
        {
           
                return result.StatusCode switch
                {
                    HttpStatusCode.OK => Results.Ok(result.Data),
                    HttpStatusCode.Created => Results.Created(result.UrlAsCreated, result.Data),
                    HttpStatusCode.BadRequest => Results.BadRequest(result.Fail!),
                    HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                    _ => Results.Problem(result.Fail!)
                };

        }
        public static IResult ToGenericResult(this ServiceResult result)
        {

            return result.StatusCode switch
            {
    
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!)
            };

        }
    }
}
