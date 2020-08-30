using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public CustomerController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(Guid id) => 
            await databaseContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get() =>
            await databaseContext.Customers.ToListAsync();
    }
}
