using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace TransactionApi.DataAccess.Data
{
    public class Seeder
    {
        public static void SeedData(TransactionContext databaseContext)
        {
            if (databaseContext.Transactions.Any())
            {
                return;
            }

            var userData = System.IO.File.ReadAllText("Data/transaction-seed.json");
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(userData);

            databaseContext.AddRange(transactions);
            databaseContext.SaveChanges();
        }
    }
}
