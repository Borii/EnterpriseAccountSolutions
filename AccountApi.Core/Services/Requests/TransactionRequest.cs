using System;

namespace AccountApi.Core.Services.Requests
{
    public class TransactionRequest
    {
        public Guid AccountId { get; set; } 
        public decimal Amount { get; set; }
    }
}
