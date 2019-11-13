using CoffeeHouse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }
        public TableRequest Request { get; set; }
    }
}
