using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeHouse.Models;
using CoffeeHouse.Data;
using CoffeeHouse.Repository.Interfaces;
using CoffeeHouse.Data.Models;
using CoffeeHouse.ViewModels;

namespace CoffeeHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var drinks = _unitOfWork.DrinkRepository.GetDrinks();
            var request = new TableRequest();
            var indexViewModel = new IndexViewModel
            {
                Drinks = drinks,
                Request = request
            };
            return View(indexViewModel);
        }

        public async Task<IActionResult> Detail(int drinkId)
        {
            return View(await _unitOfWork.DrinkRepository.GetDrinkByIdAsync(drinkId));
        }

        public IActionResult Menu()
        {
            var drinks = _unitOfWork.DrinkRepository.GetDrinks();
            var request = new TableRequest();
            var indexViewModel = new IndexViewModel
            {
                Drinks = drinks,
                Request = request
            };
            return View(indexViewModel);
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
