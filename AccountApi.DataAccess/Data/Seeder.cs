using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace AccountApi.DataAccess.Data
{
    public class Seeder
    {
        public static void SeedData(AccountContext databaseContext)
        {
            if (databaseContext.Accounts.Any())
            {
                return;
            }

            var userData = System.IO.File.ReadAllText("Data/account-seed.json");
            var accounts = JsonConvert.DeserializeObject<List<Account>>(userData);

            databaseContext.AddRange(accounts);
            databaseContext.SaveChanges();
        }
    }
}
