using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeHouse.Data;
using CoffeeHouse.Data.Dto;
using CoffeeHouse.Data.Models;
using CoffeeHouse.Repository.Interfaces;
using CoffeeHouse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoffeeHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //var drinks = _unitOfWork.DrinkRepository.GetDrinks();
            //return View(drinks);
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSort"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            List<Drink> ListSearchDrink = new List<Drink>();
            ListSearchDrink = _unitOfWork.DrinkRepository.GetDrinks().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                ListSearchDrink = ListSearchDrink.Where(s => s.DrinkName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ListSearchDrink = ListSearchDrink.OrderByDescending(s => s.DrinkName).ToList();
                    break;
                case "price_desc":
                    ListSearchDrink.OrderByDescending(s => s.Price).ToList();
                    break;
                    //case "year_desc":
                    //    model.Cheeses.OrderByDescending(s => s.Name);
                    //    break;
            }
            int pageSize = 6;
            return View(PaginatedListDrink<Drink>.Create(ListSearchDrink.AsQueryable(), page ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            ViewData["categories"] = new SelectList(categories, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(DrinkDto newDrink, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo == null || photo.Length == 0)
                {
                    newDrink.Photo = "no-image.png";
                }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Drinks", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);
                    newDrink.Photo = photo.FileName;
                }
                var addDrink = _mapper.Map<DrinkDto, Drink>(newDrink);
                await _unitOfWork.DrinkRepository.AddDrinkAsync(addDrink);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
                ViewData["categories"] = new SelectList(categories, "Id", "CategoryName");
                return View();
            }
        }

        public async Task<IActionResult> Detail(int drinkId)
        {
            return View( await _unitOfWork.DrinkRepository.GetDrinkByIdAsync(drinkId));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int drinkId)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            ViewData["categories"] = new SelectList(categories, "Id", "CategoryName");
            return View(await _unitOfWork.DrinkRepository.GetDrinkByIdAsync(drinkId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int drinkId, Drink editDrink, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo == null || photo.Length == 0)
                {
                    editDrink.Photo = "no-image.png";
                }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Drinks", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);
                    editDrink.Photo = photo.FileName;
                }
                var selectedDrink = editDrink;
                selectedDrink.Id = drinkId;
                _unitOfWork.DrinkRepository.UpdateDrink(selectedDrink);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
                ViewData["categories"] = new SelectList(categories, "Id", "CategoryName");
                return View(await _unitOfWork.DrinkRepository.GetDrinkByIdAsync(drinkId));
            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _unitOfWork.DrinkRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}