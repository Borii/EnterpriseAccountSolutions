using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Core;
using TransactionApi.DataAccess;
using TransactionApi.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionCore transactionCore;

        public TransactionController(ITransactionCore transactionCore)
        {
            this.transactionCore = transactionCore;
        }

        // GET api/<TransactionController>/5
        [HttpGet("{accountId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get(Guid accountId) =>
            Ok(await this.transactionCore.GetByAccountId(accountId));

        // POST api/<TransactionController>
        [HttpPost]
        public async Task<ActionResult<Transaction>> Post([FromBody] TransactionToCreateDto transactionToCreateDto)
        {
            return Ok(await this.transactionCore.Add(transactionToCreateDto));
        }
    }
}
