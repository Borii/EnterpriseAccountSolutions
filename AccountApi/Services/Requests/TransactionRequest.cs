using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Services.Requests
{
    public class TransactionRequest
    {
        public Guid AccountId { get; set; } 
        public decimal Amount { get; set; }
    }
}
