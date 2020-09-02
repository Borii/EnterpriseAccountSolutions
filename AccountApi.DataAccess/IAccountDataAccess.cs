using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountApi.DataAccess
{
    public interface IAccountDataAccess
    {
        Task<IEnumerable<Account>> GetAll();
        Task<IEnumerable<Account>> GetByCustomerId(Guid customerId);
        Task<Account> Add(Account accountToCreate);
    }
}