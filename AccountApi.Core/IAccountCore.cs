using AccountApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApi.Core.Models;

namespace AccountApi.Core
{
    public interface IAccountCore
    {
        Task<UserInfo> GetUserInfo(Guid customerId);
        Task Add(AccountToCreateDto accountToCreateDto);
        Task<IEnumerable<Account>> GetAll();
    }
}