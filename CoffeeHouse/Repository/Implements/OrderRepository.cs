using CoffeeHouse.Data;
using CoffeeHouse.Data.Dto;
using CoffeeHouse.Data.Models;
using CoffeeHouse.Repository.Interfaces;
using CoffeeHouse.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Repository.Implements
{
    public class OrderRepository: IOrderRepository
    {
        private readonly CoffeeDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderRepository(
            CoffeeDbContext context,
            IShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public async Task DeliveryOrder(int id)
        {
            var waitingOrder = await _context.Orders.FirstOrDefaultAsync(w => w.Id == id);
            waitingOrder.Status = true;

            await _context.SaveChangesAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            order.OrderPlacedTime = DateTime.Now;
            await _context.Orders.AddAsync(order);

            var shoppingCartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
            order.OrderTotal = (await _shoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;

            await _context.OrderDetails.AddRangeAsync(shoppingCartItems.Select(e => new OrderDetail
            {
                Qty = e.Qty,
                DrinkName = e.Drink.DrinkName,
                OrderId = order.Id,
                Price = e.Drink.Price
            }));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(e => e.OrderDetails)
                .Select(e => new MyOrderViewModel
                {
                    Id = e.Id,
                    Status = e.Status,
                    OrderPlacedTime = e.OrderPlacedTime,
                    OrderTotal = e.OrderTotal,
                    OrderPlaceDetails = new OrderDto
                    {
                        AddressLine = e.AddressLine,
                        City = e.City,
                        Country = e.Country,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        State = e.State,
                        ZipCode = e.ZipCode
                    },
                    DrinkOrderInfo = e.OrderDetails.Select(o => new MyDrinkOrderInfo
                    {
                        Name = o.DrinkName,
                        Price = o.Price,
                        Qty = o.Qty
                    })
                })
                .ToListAsync();

        }
        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId)
        {
            return await _context.Orders
                .Where(e => e.UserId == userId)
                .Include(e => e.OrderDetails)
                .Select(e => new MyOrderViewModel
                {
                    Id = e.Id,
                    Status = e.Status,
                    OrderPlacedTime = e.OrderPlacedTime,
                    OrderTotal = e.OrderTotal,
                    OrderPlaceDetails = new OrderDto
                    {
                        AddressLine = e.AddressLine,
                        City = e.City,
                        Country = e.Country,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        State = e.State,
                        ZipCode = e.ZipCode
                    },
                    DrinkOrderInfo = e.OrderDetails.Select(o => new MyDrinkOrderInfo
                    {
                        Name = o.DrinkName,
                        Price = o.Price,
                        Qty = o.Qty
                    })
                })
                .ToListAsync();
        }
    }
}
