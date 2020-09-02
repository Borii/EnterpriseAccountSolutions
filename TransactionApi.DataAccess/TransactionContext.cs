using Microsoft.EntityFrameworkCore;

namespace TransactionApi.DataAccess
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
