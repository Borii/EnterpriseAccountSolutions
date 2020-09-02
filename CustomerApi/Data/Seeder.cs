using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Data
{
    public class Seeder
    {
        public static void SeedData(DatabaseContext databaseContext)
        {
            if (databaseContext.Customers.Any())
            {
                return;
            }

            var userData = System.IO.File.ReadAllText("Data/customer-seed.json");
            var customers = JsonConvert.DeserializeObject<List<Customer>>(userData);

            databaseContext.AddRange(customers);
            databaseContext.SaveChanges();
        }
    }
}
