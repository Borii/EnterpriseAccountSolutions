using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AccountApi.DataAcess.Tests
{
    public class AccountDataAccessTests
    {
        private ServiceCollection services;
        private IServiceScope scope;
        private IAccountDataAccess accountDataAccess;

        [SetUp]
        public void Setup()
        {
            string databaseName = Guid.NewGuid().ToString();

            this.services = new ServiceCollection();
            this.services.AddDbContext<AccountContext>(o => o.UseInMemoryDatabase(databaseName: databaseName));

            var provider = this.services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                this.services.AddScoped<IAccountDataAccess, AccountDataAccess>();
            }

            this.scope = this.services.BuildServiceProvider().CreateScope();
            this.accountDataAccess = scope.ServiceProvider.GetRequiredService<IAccountDataAccess>();

        }

        [Test]
        public async Task Add()
        {
            var account = new Account
            {
                CustomerId  = Guid.NewGuid(),
                Name = "Savings01"
            };

            var createdAccount = await this.accountDataAccess.Add(account);
            Assert.IsTrue(createdAccount.Id != Guid.Empty);
            Assert.AreEqual(account.Id, createdAccount.Id);
            Assert.AreEqual(account.Name, createdAccount.Name);
            Assert.AreEqual(account.CustomerId, createdAccount.CustomerId);
        }

        [Test]
        public async Task GetAll()
        {
            var accounts = await this.accountDataAccess.GetAll();
            Assert.NotNull(accounts);
        }

        [Test]
        public async Task GetByCustomerId()
        {
            var customerId = Guid.NewGuid();
            var account = new Account
            {
                CustomerId = customerId,
                Name = "Savings02"
            };

            await this.accountDataAccess.Add(account);
            var customerAccounts = await this.accountDataAccess.GetByCustomerId(customerId);
            Assert.NotNull(customerAccounts);
            Assert.IsNotEmpty(customerAccounts);
            Assert.That(new List<Account>{account}, Is.SupersetOf(customerAccounts));
        }
    }
}