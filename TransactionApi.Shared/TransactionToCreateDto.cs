using System;

namespace TransactionApi.Shared
{
    public class TransactionToCreateDto
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
