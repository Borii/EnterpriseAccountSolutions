using System;
using AccountApi.DataAccess;

namespace AccountApi.Core.Services.Responses
{
    public class TransactionResponse
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
