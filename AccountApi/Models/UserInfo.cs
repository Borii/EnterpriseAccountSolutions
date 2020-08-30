using System.Collections.Generic;

namespace AccountApi.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<AccountDto> Accounts { get; set; }

    }
}
