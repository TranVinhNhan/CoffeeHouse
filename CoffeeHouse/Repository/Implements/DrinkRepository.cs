using CoffeeHouse.Data;
using CoffeeHouse.Data.Models;
using CoffeeHouse.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Repository.Implements
{
    public class DrinkRepository: IDrinkRepository
    {
        private readonly CoffeeDbContext _coffeeDbContext;

        public DrinkRepository(CoffeeDbContext coffeeDbContext)
        {
            _coffeeDbContext = coffeeDbContext;
        }

        public DrinkRepository()
        {

        }
        //Add
        public async Task AddDrinkAsync(Drink drink)
        {
            await _coffeeDbContext.Drinks.AddAsync(drink);
            await _coffeeDbContext.SaveChangesAsync();
        }

        //Delete
        public void Delete(int DrinkId)
        {
            var drink = new Drink { Id = DrinkId };
            _coffeeDbContext.Entry(drink).State = EntityState.Deleted;
            _coffeeDbContext.SaveChanges();
        }

        //Get by Id
        //public async Task<Drink> GetDrinkByIdAsync(int drinkId)
        //{
        //    return await _coffeeDbContext.Drinks.Include(c => c.Category).FirstOrDefaultAsync(d => d.Id == drinkId);
        //}
        public async Task<Drink> GetDrinkByIdAsync(int drinkId)
        {
            return await _coffeeDbContext.Drinks.Include(c => c.Category).FirstOrDefaultAsync(d => d.Id == drinkId);
        }

        //public Drink GetDrinkById(int drinkId)
        //{
        //    return _coffeeDbContext.Drinks.Include(c => c.Category).FirstOrDefault(d => d.Id == drinkId);
        //}

        //Get all or by category
        public IEnumerable<Drink> GetDrinks(string category = null)
        {
            //var query = _coffeeDbContext.Drinks.Include(c => c.Category).AsQueryable();
            //if (!string.IsNullOrEmpty(category))
            //{
            //    query = query.Where(c => c.Category.CategoryName == category);
            //}

            //return await query.ToListAsync();
            return _coffeeDbContext.Drinks.Include(c => c.Category);
        }

        //Update
        public void UpdateDrink(Drink drink)
        {
            _coffeeDbContext.Drinks.Update(drink);
        }
    }
}
