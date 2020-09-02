using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountApi.DataAccess;
using AccountApi.Core;
using AccountApi.Core.Models;

namespace AccountApi.Controllers
{

    /// <summary>
    ///  Account Api Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>The account core - account business logic</summary>
        private readonly IAccountCore accountCore;

        public AccountController(
            IAccountCore accountCore
            )
        {
            this.accountCore = accountCore;
        }


        /// <summary>
        /// Gets all accounts.
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get() =>
            Ok(await this.accountCore.GetAll());


        /// <summary>Gets the user information.</summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>
        ///   User info.
        /// </returns>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(Guid customerId) => 
            await this.accountCore.GetUserInfo(customerId);


        /// <summary>Adds the specified account to create.</summary>
        /// <param name="accountToCreateDto">The account to create dto.</param>
        /// <returns>
        ///   OkResult
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AccountToCreateDto accountToCreateDto)
        {
            await this.accountCore.Add(accountToCreateDto);
            return Ok();
        }
    }
}
