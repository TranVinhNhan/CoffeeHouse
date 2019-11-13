using AutoMapper;
using CoffeeHouse.Data.Dto;
using CoffeeHouse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeHouse.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<DrinkDto, Drink>();
            CreateMap<OrderDto, Order>();
        }     
    }
}
