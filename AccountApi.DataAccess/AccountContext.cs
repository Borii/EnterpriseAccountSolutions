using Microsoft.EntityFrameworkCore;

namespace AccountApi.DataAccess
{

    /// <summary>
    ///   <br />
    /// </summary>
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
            
        public DbSet<Account> Accounts { get; set; }
    }
}
