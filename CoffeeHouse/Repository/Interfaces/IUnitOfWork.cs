using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Repository.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IDrinkRepository DrinkRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ITableRepository TableRepository { get; }
        Task SaveAsync();
    }
}
