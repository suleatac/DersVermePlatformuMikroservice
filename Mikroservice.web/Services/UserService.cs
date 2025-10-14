using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.web.Services
{
    public class UserService(IHttpContextAccessor httpContextAccessor) 
    {
        public Guid UserId
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new Exception("User is not authenticated");
                }
                return Guid.Parse(httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(c => c.Type == "sub")?.Value ?? Guid.Empty
                    .ToString());
            }



        }
        public string Username
        {
            get
            {
                if(!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                throw new Exception("User is not authenticated");
                }
                return httpContextAccessor.HttpContext?.User.Identity!.Name!;
            }



        }
        public List<string> Roles
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    throw new Exception("User is not authenticated");
                }
                return httpContextAccessor.HttpContext!.User.Claims.Where(x=>x.Type==ClaimTypes.Role).Select(x=>x.Value!).ToList();
            }



        }
    }
}
