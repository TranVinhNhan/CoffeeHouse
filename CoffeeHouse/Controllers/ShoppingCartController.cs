using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeHouse.Repository.Interfaces;
using CoffeeHouse.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeHouse.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartController(IUnitOfWork unitOfWork, IShoppingCartService shoppingCartService)
        {
            _unitOfWork = unitOfWork;
            _shoppingCart = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            await _shoppingCart.GetShoppingCartItemsAsync();
            var shoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmmountAsync();
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = shoppingCartCountTotal.TotalAmmount,
            };


            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int drinkId)
        {
            var selectedDrink = await _unitOfWork.DrinkRepository.GetDrinkByIdAsync(drinkId);
            if (selectedDrink == null)
            {
                return NotFound();
            }

            await _shoppingCart.AddToCartAsync(selectedDrink);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromShoppingCart(int drinkId)
        {
            var selectedDrink = await _unitOfWork.DrinkRepository.GetDrinkByIdAsync(drinkId);
            if (selectedDrink == null)
            {
                return NotFound();
            }

            await _shoppingCart.RemoveFromCartAsync(selectedDrink);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllCart()
        {
            await _shoppingCart.ClearCartAsync();
            return RedirectToAction("Index");
        }
    }
}