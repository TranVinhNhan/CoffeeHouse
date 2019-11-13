using CoffeeHouse.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.ViewModels
{
    public class MyOrderViewModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public OrderDto OrderPlaceDetails { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlacedTime { get; set; }
        public IEnumerable<MyDrinkOrderInfo> DrinkOrderInfo { get; set; }

    }

    public class MyDrinkOrderInfo
    {
        public int Qty { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }
}
