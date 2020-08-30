using System;

namespace TransactionApi.DataAccess
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}