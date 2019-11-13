using CoffeeHouse.Components;
using CoffeeHouse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Repository.Interfaces
{
    public interface ITableRepository
    {
        IEnumerable<TableRequest> GetRequests();
        Task AddRequestAsync(TableRequest request);
        Task CheckRequest(int requestId, string status);
    }
}
