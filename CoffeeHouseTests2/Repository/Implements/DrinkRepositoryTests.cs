using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeHouse.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using CoffeeHouse.Data.Models;
using CoffeeHouse.Data;
using CoffeeHouse.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace CoffeeHouse.Repository.Implements.Tests
{

    [TestClass()]
    public class DrinkRepositoryTests
    {
        [TestMethod()]
        public async Task GetDrinkByIdAsyncTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            using (var context = new CoffeeDbContext(options))
            {
                var newDrink = new Drink { DrinkName = "test", Description = "test", Price = 100, CategoryId = 1, Category = new Category() };
                var service = new DrinkRepository(context);
                await service.AddDrinkAsync(newDrink);
            }

            using (var context = new CoffeeDbContext(options))
            {
                var newDrink = new Drink { Id = 1, DrinkName = "test", Description = "test", Price = 100, CategoryId = 1, Category = new Category() };
                var service = new DrinkRepository(context);
                var result = await service.GetDrinkByIdAsync(1);
                Assert.AreEqual(1, context.Drinks.Count());
                Assert.AreEqual(newDrink.Id, result.Id);
            }
        }

        [TestMethod()]
        public async Task AddDrinkAsyncTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            using (var context = new CoffeeDbContext(options))
            {
                var newDrink = new Drink { DrinkName = "test", Description = "test", Price = 100, CategoryId = 1 };
                var service = new DrinkRepository(context);
                await service.AddDrinkAsync(newDrink);
            }

            using (var context = new CoffeeDbContext(options))
            {
                var newDrink = new Drink { DrinkName = "test", Description = "test", Price = 100, CategoryId = 1 };
                var result = context.Drinks.Single();
                //Assert.IsNull(result);
                Assert.AreEqual(1, context.Drinks.Count());
                Assert.AreEqual(newDrink.DrinkName, result.DrinkName);
            }
        }

        [TestMethod()]
        public async Task DeleteTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            using (var context = new CoffeeDbContext(options))
            {
                var newDrink = new Drink { DrinkName = "test", Description = "test", Price = 100, CategoryId = 1 };
                var service = new DrinkRepository(context);
                await service.AddDrinkAsync(newDrink);
            }

            using (var context = new CoffeeDbContext(options))
            {
                var service = new DrinkRepository(context);
                Assert.AreEqual(1, context.Drinks.Count());
                service.Delete(1);
                Assert.AreEqual(0, context.Drinks.Count());
            }
        }

        [TestMethod()]
        public async Task GetDrinksTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;

            using (var context = new CoffeeDbContext(options))
            {
                var newDrink1 = new Drink { DrinkName = "test", Description = "test", Price = 100, CategoryId = 1, Category = new Category() };
                var newDrink2 = new Drink { DrinkName = "test2", Description = "test2", Price = 200, CategoryId = 2, Category = new Category() };
                var service = new DrinkRepository(context);
                await service.AddDrinkAsync(newDrink1);
                await service.AddDrinkAsync(newDrink2);
            }

            using (var context = new CoffeeDbContext(options))
            {
                var service = new DrinkRepository(context);
                var result = service.GetDrinks();
                Assert.AreEqual(2, result.Count());
            }
        }
    }
}