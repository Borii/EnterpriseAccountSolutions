using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Models
{
    public class TransactionToCreateDto
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
