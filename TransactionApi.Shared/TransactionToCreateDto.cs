using System;

namespace TransactionApi.DataAccess.Dtos
{
    public class TransactionToCreateDto
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
