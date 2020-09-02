using System;

namespace AccountApi.DataAccess
{
    public class AccountResponse
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}