using System.Collections.Generic;

namespace AccountApi.Models
{
    public class AccountDto
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; }
    }
}