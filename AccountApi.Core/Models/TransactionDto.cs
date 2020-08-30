using System;

namespace AccountApi.Models
{
    public class TransactionDto
    {
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}