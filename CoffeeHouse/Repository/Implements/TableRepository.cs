using CoffeeHouse.Components;
using CoffeeHouse.Data;
using CoffeeHouse.Data.Models;
using CoffeeHouse.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Repository.Implements
{
    public class TableRepository : ITableRepository
    {
        private readonly CoffeeDbContext _coffeeDbContext;

        public TableRepository(CoffeeDbContext coffeeDbContext)
        {
            _coffeeDbContext = coffeeDbContext;
        }

        public async Task AddRequestAsync(TableRequest request)
        {
            await _coffeeDbContext.TableRequests.AddAsync(request);
            await _coffeeDbContext.SaveChangesAsync();
        }

        public async Task CheckRequest(int requestId, string status)
        {
            var waitingRequest = await _coffeeDbContext.TableRequests.FindAsync(requestId);
            waitingRequest.Status = status;
            await _coffeeDbContext.SaveChangesAsync();
        }

        public IEnumerable<TableRequest> GetRequests()
        {
            return _coffeeDbContext.TableRequests;
        }
    }
}
