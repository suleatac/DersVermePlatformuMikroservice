using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Bus
{
    public class BusOption
    
        {
        public required string Address { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Port { get; set; }
       
    }
}
