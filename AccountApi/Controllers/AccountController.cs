using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApi.Models;
using Microsoft.AspNetCore.Mvc;
using AccountApi.DataAccess;
using AccountApi.Core;

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
        public async Task<ActionResult<IEnumerable<AccountResponse>>> Get() =>
            Ok(await this.accountCore.GetAll());

        [HttpGet("{customerId}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(Guid customerId) => 
            await this.accountCore.GetUserInfo(customerId);

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AccountToCreateDto accountToCreateDto)
        {
            await this.accountCore.Add(accountToCreateDto);
            return Ok();
        }
    }
}
