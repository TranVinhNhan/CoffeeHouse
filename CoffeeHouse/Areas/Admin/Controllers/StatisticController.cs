using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticController : Controller
    {
        private readonly CoffeeDbContext _context;

        public StatisticController(CoffeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["BanDuoc"] = _context.Orders.Count();
            double tong = 0;
            var allOrders = _context.Orders.ToList();
            foreach (var o in allOrders)
                tong = tong + o.OrderTotal;
            ViewData["TongTien"] = tong;
            ViewData["TongSoMonAn"] = _context.Drinks.Count();
            return View();
        }
    }
}