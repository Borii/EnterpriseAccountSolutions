using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApi.Core.Services;
using AccountApi.Core.Services.Responses;
using AccountApi.DataAccess;
using Moq;
using NUnit.Framework;

namespace AccountApi.Core.Tests
{
    public class AccountCoreTests
    {

        [Test]
        public async Task GetUserInfo()
        {
        var accountDataAccessMoq = new Mock<IAccountDataAccess>();
        var customerClientMoq = new Mock<ICustomerClient>();
        var transactionClientMoq = new Mock<ITransactionClient>();

        var accountCore = new AccountCore(accountDataAccessMoq.Object, customerClientMoq.Object, transactionClientMoq.Object);

        var expectedCustomer = new CustomerResponse()
        {
            Name = "John",
            Surname = "Doe"
        };

        var customerId = Guid.NewGuid();
        var expectedAccount1 = new Account()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Name = "Account01"
        };
        var expectedAccount2 = new Account()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Name = "Account02"
        };
        var expectedAccount3 = new Account()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Name = "Account03"
        };

        var accountsList = new List<Account>() { expectedAccount1, expectedAccount2, expectedAccount2 };

        customerClientMoq.Setup(x => x.GetById(customerId.ToString())).Returns(async () =>
            await Task.FromResult(expectedCustomer)
        );

        accountDataAccessMoq.Setup(x => x.GetByCustomerId(customerId)).Returns(async () =>
            await Task.FromResult(accountsList));

        var expectedTransactions1 = new List<TransactionResponse>() {new TransactionResponse()
        {
            AccountId = expectedAccount1.Id,
            Amount = 50,
            Timestamp = DateTime.Now
        }, new TransactionResponse()
        {
            AccountId = expectedAccount1.Id,
            Amount = 500,
            Timestamp = DateTime.Now
        }, new TransactionResponse(){
            AccountId = expectedAccount1.Id,
            Amount = 500,
            Timestamp = DateTime.Now
        }};

        transactionClientMoq.Setup(x => x.Get(expectedAccount1.Id.ToString())).Returns(async () =>
            await Task.FromResult(expectedTransactions1));

        var expectedTransactions2 = new List<TransactionResponse>() { new TransactionResponse()
        {

        }, new TransactionResponse()
        {
            AccountId = expectedAccount2.Id,
            Amount = 25,
            Timestamp = DateTime.Now
        }, new TransactionResponse()
        {
            AccountId = expectedAccount2.Id,
            Amount = 250,
            Timestamp = DateTime.Now
        }};

        transactionClientMoq.Setup(x => x.Get(expectedAccount2.Id.ToString())).Returns(async () =>
        await Task.FromResult(expectedTransactions2));

        var resultUserInfo = await accountCore.GetUserInfo(customerId);

        Assert.NotNull(resultUserInfo);
        var expectedTransactionsCount = expectedTransactions1.Count + expectedTransactions2.Count;

        }
    }
}