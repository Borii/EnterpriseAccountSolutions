using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionApi.DataAccess.Dtos;

namespace TransactionApi.DataAccess
{
    public class TransactionDataAccess : ITransactionDataAccess
    {
        private readonly TransactionContext transactionContext;

        public TransactionDataAccess(TransactionContext transactionContext)
        {
            this.transactionContext = transactionContext;
        }

        public async Task<IEnumerable<Transaction>> GetByAccountId(Guid accountId) =>
            await transactionContext.Transactions.Where(x => x.AccountId == accountId).ToListAsync();

        public async Task<Transaction> Add(TransactionToCreateDto transactionToCreateDto)
        {
            var transaction = new Transaction { AccountId = transactionToCreateDto.AccountId, Amount = transactionToCreateDto.Amount, Timestamp = DateTime.Now };
            await transactionContext.AddAsync(transaction);
            await transactionContext.SaveChangesAsync();
            return await transactionContext.Transactions.FirstAsync(t => t.Id == transaction.Id);
        }


    }
}
