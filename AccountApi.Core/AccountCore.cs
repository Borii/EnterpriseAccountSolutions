using AccountApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApi.Core.Models;
using AccountApi.Core.Services;
using AccountApi.Core.Services.Requests;

namespace AccountApi.Core
{
    public class AccountCore : IAccountCore
    {
        private readonly IAccountDataAccess accountDataAccess;
        private readonly ICustomerClient customerClient;
        private readonly ITransactionClient transactionClient;

        public AccountCore(IAccountDataAccess accountDataAccess, ICustomerClient customerClient, ITransactionClient transactionClient)
        {
            this.accountDataAccess = accountDataAccess;
            this.customerClient = customerClient;
            this.transactionClient = transactionClient;
        }

        public async Task<UserInfo> GetUserInfo(Guid customerId)
        {
            var customerResponse = await customerClient.GetById(customerId.ToString());

            var accounts = await this.accountDataAccess.GetByCustomerId(customerId);

            var transactions = accounts.Select(async account => await this.transactionClient.Get(account.Id.ToString()))
                .Select(task => task.Result).SelectMany(x => x.ToList());

            var transactionsByAccountId = transactions.ToLookup(transaction => transaction.AccountId, transaction => transaction);

            var userInfo = new UserInfo
            {
                Name = customerResponse.Name,
                Surname = customerResponse.Surname,
                Balance = transactionsByAccountId.Sum(transaction => transaction.Sum(t => t.Amount)),
                Accounts = accounts.Select(account => new AccountDto
                {
                    Name = account.Name,
                    Balance = transactionsByAccountId[account.Id].Sum(transaction => transaction.Amount),
                    Transactions = transactionsByAccountId[account.Id].Select(transaction => new TransactionDto
                    {
                        Amount = transaction.Amount,
                        Timestamp = transaction.Timestamp
                    })
                })
            };

            return userInfo;
        }

        public async Task Add(AccountToCreateDto accountToCreateDto)
        {
            var accountToCreate = new Account { CustomerId = accountToCreateDto.CustomerId.Value, Name = accountToCreateDto.Name };
            var createdAccount = await accountDataAccess.Add(accountToCreate);

            if (accountToCreateDto.InitialCredit > 0)
            {
                var transactionRequest = new TransactionRequest { AccountId = createdAccount.Id, Amount = accountToCreateDto.InitialCredit };
                await transactionClient.Post(transactionRequest);
            }
        }

        public async Task<IEnumerable<Account>> GetAll() => await this.accountDataAccess.GetAll();
    }
}
