using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApi.Controllers;
using AccountApi.Core;
using AccountApi.Core.Models;
using AccountApi.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AccountApi.Tests
{
    public class AccountControllerTests
    {
        [Test]
        public async Task Get_CheckResult()
        {
            var accountCoreMoq = new Mock<IAccountCore>();
            var controller = new AccountController(accountCoreMoq.Object);
            var result = await controller.Get();
            Assert.That(result is ActionResult<IEnumerable<Account>>);
        }

        [Test]
        public async Task Get()
        {
            var accountCoreMoq = new Mock<IAccountCore>();
            var controller = new AccountController(accountCoreMoq.Object);
            

            var accountList = new List<Account>()
            {
                new Account {CustomerId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "account1"},
                new Account {CustomerId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "account2"},
                new Account {CustomerId = Guid.NewGuid(), Id = Guid.NewGuid(), Name = "account3"}
            };

            accountCoreMoq.Setup(x => x.GetAll()).Returns(async () =>
                await Task.FromResult(accountList)
            );

            var result = await controller.Get();

            Assert.NotNull(result);
            Assert.That((((OkObjectResult) result.Result).Value as IEnumerable<Account>).Count() == accountList.Count());
        }

        [Test]
        public async Task GetUserInfo()
        {
            var accountCoreMoq = new Mock<IAccountCore>();
            var controller = new AccountController(accountCoreMoq.Object);

            var customerId = Guid.NewGuid();

            var accountList = new List<AccountDto>()
            {
                new AccountDto {Balance = 10, Name = "account1"},
                new AccountDto {Balance = 100, Name = "account2"},
                new AccountDto {Balance = 10000, Name = "account3"}
            };

            var userInfo = new UserInfo()
            {
                Accounts = accountList,
                Name = "Ann",
                Surname = "Taylor",
                Balance = accountList.Sum(x => x.Balance)
            };

            accountCoreMoq.Setup(x => x.GetUserInfo(customerId)).Returns(async () =>
                await Task.FromResult(userInfo)
            );

            var result = await controller.GetUserInfo(customerId);

            Assert.NotNull(result);
            var resultUserInfo = ((OkObjectResult) result.Result).Value as UserInfo;
            Assert.NotNull(resultUserInfo);
            Assert.AreEqual(userInfo.Balance, resultUserInfo.Balance);
            Assert.AreEqual(userInfo.Name, resultUserInfo.Name);
            Assert.AreEqual(userInfo.Surname, resultUserInfo.Surname);
            Assert.AreEqual(userInfo.Accounts.Count(), resultUserInfo.Accounts.Count());
        }

        [Test]
        public async Task Add()
        {
            var accountCoreMoq = new Mock<IAccountCore>();
            var controller = new AccountController(accountCoreMoq.Object);

            var accountToCreateDto = new AccountToCreateDto()
            {
                CustomerId = Guid.NewGuid(),
                InitialCredit = 300.00m,
                Name = "Savings01"
            };

            accountCoreMoq.Setup(x => x.Add(accountToCreateDto)).Returns(async () =>
                await Task.CompletedTask
            );

            var result = await controller.Add(accountToCreateDto);

            Assert.NotNull(result);
            Assert.That((result is OkResult));

        }
    }
}
