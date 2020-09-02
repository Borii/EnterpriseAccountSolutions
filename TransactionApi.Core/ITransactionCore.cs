using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionApi.DataAccess;
using TransactionApi.Shared;

namespace TransactionApi.Core
{
    public interface ITransactionCore
    {
        Task<IEnumerable<Transaction>> GetByAccountId(Guid accountId);
        Task<Transaction> Add(TransactionToCreateDto transactionToCreateDto);
    }
}
