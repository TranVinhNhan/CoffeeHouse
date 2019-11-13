using CoffeeHouse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Repository.Interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> GetDrinks(string category = null);
        Task<Drink> GetDrinkByIdAsync(int drinkId);
        //Drink GetDrinkById(int drinkId);
        void UpdateDrink(Drink drink);
        Task AddDrinkAsync(Drink drink);
        void Delete(int DrinkId);
    }
}
