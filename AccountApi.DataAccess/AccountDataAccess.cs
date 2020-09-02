using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.DataAccess
{
    public class AccountDataAccess : IAccountDataAccess
    {
        private readonly AccountContext databaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountDataAccess"/> class.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        public AccountDataAccess(AccountContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Account> Add(Account accountToCreate)
        {
            await databaseContext.AddAsync(accountToCreate);
            await databaseContext.SaveChangesAsync();
            return await databaseContext.Accounts.FirstAsync(account => account.Id == accountToCreate.Id); ;
        }

        public async Task<IEnumerable<Account>> GetAll() =>
            await databaseContext.Accounts.ToListAsync();

        public async Task<IEnumerable<Account>> GetByCustomerId(Guid customerId) =>
            await databaseContext.Accounts.Where(account => account.CustomerId == customerId).ToListAsync();
    }
}
