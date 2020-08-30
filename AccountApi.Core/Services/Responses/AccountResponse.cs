using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Services.Responses
{
    public class AccountResponse
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}
