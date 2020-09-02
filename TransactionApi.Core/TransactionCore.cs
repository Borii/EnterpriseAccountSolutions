using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionApi.DataAccess;
using TransactionApi.Shared;

namespace TransactionApi.Core
{
    public class TransactionCore : ITransactionCore
    {
        private readonly ITransactionDataAccess transactionDataAccess;

        public TransactionCore(ITransactionDataAccess transactionDataAccess)
        {
            this.transactionDataAccess = transactionDataAccess;
        }

        public async Task<IEnumerable<Transaction>> GetByAccountId(Guid accountId) => 
            await this.transactionDataAccess.GetByAccountId(accountId);

        public async Task<Transaction> Add(TransactionToCreateDto transactionToCreateDto) => 
            await transactionDataAccess.Add(transactionToCreateDto);
    }
}
