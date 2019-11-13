using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeHouse.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CoffeeHouse.Data;
using CoffeeHouse.Data.Models;
using System.Linq;

namespace CoffeeHouse.Repository.Implements.Tests
{
    [TestClass()]
    public class TableRepositoryTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task AddRequestAsyncTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            using (var context = new CoffeeDbContext(options))
            {
                var service = new TableRepository(context);

                TableRequest table = new TableRequest
                {
                    Id = 1,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Date = DateTime.Now.ToString(),
                    Status = "Waiting",
                    Note = "Test",
                    PhoneNumber = "123456",
                    Time = DateTime.Now.AddDays(2).ToString()
                };

                await service.AddRequestAsync(table);
            }

            using (var context = new CoffeeDbContext(options))
            {
                Assert.AreEqual(1, context.TableRequests.Count());
            }
        }

        [TestMethod()]
        public async System.Threading.Tasks.Task CheckRequestTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            using (var context = new CoffeeDbContext(options))
            {
                var service = new TableRepository(context);

                TableRequest table = new TableRequest
                {
                    Id = 1,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Date = DateTime.Now.ToString(),
                    Status = "Waiting",
                    Note = "Test",
                    PhoneNumber = "123456",
                    Time = DateTime.Now.AddDays(2).ToString()
                };

                await service.AddRequestAsync(table);
            }

            using (var context = new CoffeeDbContext(options))
            {
                Assert.AreEqual(1, context.TableRequests.Count());

                var service = new TableRepository(context);
                await service.CheckRequest(1, "Done");

                Assert.AreEqual("Done", context.TableRequests.Single().Status);
            }
        }

        [TestMethod()]
        public async System.Threading.Tasks.Task GetRequestsTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            using (var context = new CoffeeDbContext(options))
            {
                var service = new TableRepository(context);

                TableRequest table1 = new TableRequest
                {
                    Id = 1,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Date = DateTime.Now.ToString(),
                    Status = "Waiting",
                    Note = "Test",
                    PhoneNumber = "123456",
                    Time = DateTime.Now.AddDays(2).ToString()
                };

                TableRequest table2 = new TableRequest
                {
                    Id = 2,
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    Date = DateTime.Now.ToString(),
                    Status = "Waiting1",
                    Note = "Test1",
                    PhoneNumber = "123456",
                    Time = DateTime.Now.AddDays(2).ToString()
                };

                await service.AddRequestAsync(table1);
                await service.AddRequestAsync(table2);
            }

            using (var context = new CoffeeDbContext(options))
            {
                Assert.AreEqual(2, context.TableRequests.Count());

                var service = new TableRepository(context);
                var result = service.GetRequests();

                Assert.AreEqual(2, result.Count());
            }
        }
    }
}