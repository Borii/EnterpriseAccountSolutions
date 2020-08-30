using AccountApi.DataAccess;
using AccountApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountApi.Core
{
    public interface IAccountCore
    {
        Task<UserInfo> GetUserInfo(Guid customerId);
        Task Add(AccountToCreateDto accountToCreateDto);
        Task<IEnumerable<Account>> GetAll();
    }
}