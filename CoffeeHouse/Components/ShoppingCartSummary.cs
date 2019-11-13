using CoffeeHouse.Repository.Interfaces;
using CoffeeHouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartSummary(IShoppingCartService shoppingCartService)
        {
            _shoppingCart = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ShoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmmountAsync();
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartItemsTotal = ShoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = ShoppingCartCountTotal.TotalAmmount
            };
            return View(shoppingCartViewModel);
        }
    }

}
