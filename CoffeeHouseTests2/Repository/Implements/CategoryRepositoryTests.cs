using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeHouse.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CoffeeHouse.Data.Models;
using Moq;
//using Microsoft.EntityFrameworkCore;
using CoffeeHouse.Repository.Interfaces;
using System.Threading.Tasks;
using CoffeeHouse.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CoffeeHouse.Repository.Implements.Tests
{
    [TestClass()]
    public class CategoryRepositoryTests
    {
        #region
        //private Mock<CoffeeDbContext> CreateDbContext()
        //{
        //    IQueryable<Category> categories = new List<Category>
        //    {
        //        new Category
        //        {
        //            CategoryName = "Đồ ăn",
        //            Drinks = new List<Drink>()
        //        },
        //        new Category
        //        {
        //            CategoryName = "Thức uống",
        //            Drinks = new List<Drink>()
        //        },
        //        new Category
        //        {
        //            CategoryName = "Coffee",
        //            Drinks = new List<Drink>()
        //        },
        //        new Category
        //        {
        //            CategoryName = "Tráng miệng",
        //            Drinks = new List<Drink>()
        //        }
        //    }.AsQueryable();

        //    var dbSet = new Mock<DbSet<Category>>();
        //    dbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
        //    dbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
        //    dbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
        //    dbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        //    var dbContext = new Mock<CoffeeDbContext>();
        //    dbContext.SetupGet(c => c.Categories).Returns(dbSet.Object);
        //    return dbContext;
        //}


        //private CoffeeDbContext CreateDbContextInMemory()
        //{
        //    var options = new DbContextOptionsBuilder<CoffeeDbContext>()
        //              .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //              .Options;
        //    var context = new CoffeeDbContext(options);

        //    var doanCategory = new Category
        //    {
        //        CategoryName = "Đồ ăn",
        //        Drinks = new List<Drink>()
        //    };

        //    var thucuongCategory = new Category
        //    {
        //        CategoryName = "Thức uống",
        //        Drinks = new List<Drink>()
        //    };

        //    var coffeeCategory = new Category
        //    {
        //        CategoryName = "Coffee",
        //        Drinks = new List<Drink>()
        //    };

        //    var trangmiengCategory = new Category
        //    {
        //        CategoryName = "Tráng miệng",
        //        Drinks = new List<Drink>()
        //    };

        //    context.Categories.Add(doanCategory);
        //    context.Categories.Add(thucuongCategory);
        //    context.Categories.Add(coffeeCategory);
        //    context.Categories.Add(trangmiengCategory);

        //    return context;
        //}
        #endregion
        [TestMethod()]
        public async Task GetCategoriesAsyncTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            using (var context = new CoffeeDbContext(options))
            {
                var doanCategory = new Category
                {
                    CategoryName = "Đồ ăn",
                    Drinks = new List<Drink>()
                };

                var thucuongCategory = new Category
                {
                    CategoryName = "Thức uống",
                    Drinks = new List<Drink>()
                };

                var coffeeCategory = new Category
                {
                    CategoryName = "Coffee",
                    Drinks = new List<Drink>()
                };

                var trangmiengCategory = new Category
                {
                    CategoryName = "Tráng miệng",
                    Drinks = new List<Drink>()
                };

                context.Categories.Add(doanCategory);
                context.Categories.Add(thucuongCategory);
                context.Categories.Add(coffeeCategory);
                context.Categories.Add(trangmiengCategory);
                context.SaveChanges();
            }

            using (var context = new CoffeeDbContext(options))
            {
                var service = new CategoryRepository(context);

                var result = await service.GetCategoriesAsync();
                //Assert.AreEqual(4, context.Categories.Count());
                Assert.AreEqual(4, result.Count());
            }
        }

        [TestMethod()]
        public async Task GetCategoryByIdAsyncTestAsync()
        {
            var options = new DbContextOptionsBuilder<CoffeeDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            using (var context = new CoffeeDbContext(options))
            {
                var doanCategory = new Category
                {
                    Id = 1,
                    CategoryName = "Đồ ăn",
                    Drinks = new List<Drink>()
                };

                var thucuongCategory = new Category
                {
                    Id = 2,
                    CategoryName = "Thức uống",
                    Drinks = new List<Drink>()
                };

                var coffeeCategory = new Category
                {
                    Id = 3,
                    CategoryName = "Coffee",
                    Drinks = new List<Drink>()
                };

                var trangmiengCategory = new Category
                {
                    Id = 4,
                    CategoryName = "Tráng miệng",
                    Drinks = new List<Drink>()
                };

                context.Categories.Add(doanCategory);
                context.Categories.Add(thucuongCategory);
                context.Categories.Add(coffeeCategory);
                context.Categories.Add(trangmiengCategory);
                context.SaveChanges();
            }

            using (var context = new CoffeeDbContext(options))
            {
                var service = new CategoryRepository(context);

                var result = await service.GetCategoryByIdAsync(4);
                //Assert.AreEqual(4, context.Categories.Count());
                Assert.AreEqual(4, result.Id);
            }
        }
    }
}