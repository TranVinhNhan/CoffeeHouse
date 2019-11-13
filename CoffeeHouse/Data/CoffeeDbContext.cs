using CoffeeHouse.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Data
{
    public class CoffeeDbContext: IdentityDbContext<IdentityUser>
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
        {

        }

        public CoffeeDbContext()
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<TableRequest> TableRequests { get; set; }
    }
}
