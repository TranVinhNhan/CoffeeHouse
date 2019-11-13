using CoffeeHouse.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IShoppingCartService ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
        public int ShoppingCartItemsTotal { get; set; }
    }
}
