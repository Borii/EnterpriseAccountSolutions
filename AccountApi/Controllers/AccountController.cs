using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountApi.DataAccess;
using AccountApi.Core;
using AccountApi.Core.Models;

namespace AccountApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCore accountCore;

        public AccountController(
            IAccountCore accountCore
            )
        {
            this.accountCore = accountCore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get() =>
            Ok(await this.accountCore.GetAll());

        [HttpGet("{customerId}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(Guid customerId) => 
            await this.accountCore.GetUserInfo(customerId);

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AccountToCreateDto accountToCreateDto)
        {
            await this.accountCore.Add(accountToCreateDto);
            return Ok();
        }
    }
}
