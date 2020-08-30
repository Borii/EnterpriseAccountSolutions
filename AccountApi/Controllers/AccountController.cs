using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApi.Models;
using AccountApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AccountApi.Services.Responses;
using System.Linq;
using AccountApi.Services.Requests;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace AccountApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DatabaseContext databaseContext;
        private readonly CustomerClient customerClient;
        private readonly TransactionClient transactionClient;

        public AccountController(
            ILogger<AccountController> logger,
            DatabaseContext databaseContext,
            CustomerClient customerClient, TransactionClient transactionClient)
        {
            _logger = logger;
            this.databaseContext = databaseContext;
            this.customerClient = customerClient;
            this.transactionClient = transactionClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> Get()
        {
            var accounts = await databaseContext.Accounts.ToListAsync();
            return accounts;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(Guid customerId)
        {
            var customerResponse = await customerClient.HttpClient.GetStringAsync(customerId.ToString());
            var customer = JsonConvert.DeserializeObject<CustomerResponse>(customerResponse);

            var accounts = await databaseContext.Accounts.Where(account => account.CustomerId == customerId).ToListAsync();

            var transactionsResponses = accounts.Select(async account => await transactionClient.HttpClient.GetStringAsync(account.Id.ToString())).Select(task => task.Result);
            var transactions = transactionsResponses.SelectMany(transactionsResponse => JsonConvert.DeserializeObject<List<TransactionResponse>>(transactionsResponse));

            var userInfo = new UserInfo
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Balance = transactions.Sum(transaction => transaction.Amount),
                Accounts = accounts.Select(account => new AccountDto
                {
                    Name = account.Name,
                    Balance = transactions.Where(transaction => transaction.AccountId == account.Id).Sum(transaction => transaction.Amount),
                    Transactions = transactions.Where(transaction => transaction.AccountId == account.Id).Select(transaction => new TransactionDto
                    {
                        Amount = transaction.Amount,
                        Timestamp = transaction.Timestamp
                    })
                })
            };

            return userInfo;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AccountToCreateDto accountToCreateDto)
        {
            var accountToCreate = new Account { CustomerId = accountToCreateDto.CustomerId.Value, Name = accountToCreateDto.Name };
            databaseContext.Add(accountToCreate);
            databaseContext.SaveChanges();

            var createdAccount = databaseContext.Accounts.First(account => account.Id == accountToCreate.Id);

            if (accountToCreateDto.InitialCredit > 0)
            {
                var transactionRequest = new TransactionRequest { AccountId = createdAccount.Id, Amount = accountToCreateDto.InitialCredit };
                await transactionClient.HttpClient.PostAsync(string.Empty, new ObjectContent<TransactionRequest>(transactionRequest, new JsonMediaTypeFormatter()));
            }

            Console.WriteLine($"{accountToCreateDto.CustomerId} {accountToCreateDto.InitialCredit}");
            return Ok();
        }
    }
}
