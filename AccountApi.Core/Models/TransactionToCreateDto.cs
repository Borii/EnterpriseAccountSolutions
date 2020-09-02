using System;

namespace AccountApi.Core.Models
{
    public class TransactionToCreateDto
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
