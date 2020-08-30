using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public TransactionController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransactionController>/5
        [HttpGet("{accountId}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get(Guid accountId)
        {
            var transactions = await databaseContext.Transactions.Where(x => x.AccountId == accountId).ToListAsync();
            return transactions;
        }

        // POST api/<TransactionController>
        [HttpPost]
        public void Post([FromBody] TransactionToCreateDto transactionToCreateDto)
        {
            Console.WriteLine("Inserting a new transaction");
            databaseContext.Add(new Transaction { AccountId = transactionToCreateDto.AccountId, Amount = transactionToCreateDto.Amount, Timestamp = DateTime.Now });
            databaseContext.SaveChanges();
            // CreatedAtRoute
        }
    }
}
