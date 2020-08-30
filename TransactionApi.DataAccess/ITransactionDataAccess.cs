using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionApi.DataAccess.Dtos;

namespace TransactionApi.DataAccess
{
    public interface ITransactionDataAccess
    {
        Task<IEnumerable<Transaction>> GetByAccountId(Guid accountId);
        Task<Transaction> Add(TransactionToCreateDto transactionToCreateDto);
    }
}