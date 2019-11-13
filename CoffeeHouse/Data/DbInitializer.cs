using CoffeeHouse.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CoffeeDbContext>();

                var doan = new Category
                {
                    CategoryName = "Đồ ăn",
                    Drinks = new List<Drink>()
                };

                var thucuong = new Category
                {
                    CategoryName = "Thức uống",
                    Drinks = new List<Drink>()
                };

                var coffee = new Category
                {
                    CategoryName = "Coffee",
                    Drinks = new List<Drink>()
                };

                var trangmieng = new Category
                {
                    CategoryName = "Tráng miệng",
                    Drinks = new List<Drink>()
                };

                context.Categories.Add(doan);
                context.Categories.Add(thucuong);
                context.Categories.Add(coffee);
                context.Categories.Add(trangmieng);

                context.SaveChanges();
            }
        }
    }
}
