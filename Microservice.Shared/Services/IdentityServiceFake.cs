using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid UserId => Guid.Parse("135df77f-8fbc-45c3-82f3-fcd4d640c8ed");

        public string Username => "testuser";
        public List<string> Roles => [];
    }
}
