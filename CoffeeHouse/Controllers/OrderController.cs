using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeHouse.Data.Dto;
using CoffeeHouse.Data.Models;
using CoffeeHouse.Repository.Interfaces;
using CoffeeHouse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeHouse.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    public class OrderController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(
            IShoppingCartService shoppingCartService,
            IMapper mapper,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Checkout()
        {
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
            if (cartItems?.Count() <= 0)
            {
                return Redirect("/shoppingcart");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromForm]OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return View(orderDto);
            }

            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();

            if (cartItems?.Count() <= 0)
            {
                ModelState.AddModelError("", "Your Cart is empty. Please add some cakes before checkout");
                return View(orderDto);
            }

            var order = _mapper.Map<OrderDto, Order>(orderDto);
            order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderRepository.CreateOrderAsync(order);

            await _shoppingCartService.ClearCartAsync();


            return View("CheckoutComplete");
        }

        [AllowAnonymous]
        public async Task<IActionResult> MyOrder()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepository.GetAllOrdersAsync(userId);
            var listOrdersVM = new ListOrdersWithId
            {
                Orders = new List<OrderWithId>()
            };
            foreach (var item in orders)
            {
                var newOrder = new OrderWithId
                {
                    MyOrderViewModel = item,
                    Id = item.Id
                };
                listOrdersVM.Orders.Add(newOrder);
            }
            return View(listOrdersVM);
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            ViewBag.ActionTitle = "All Orders";
            var orders = await _orderRepository.GetAllOrdersAsync();
            var listOrdersVM = new ListOrdersWithId
            {
                Orders = new List<OrderWithId>()
            };
            foreach (var item in orders)
            {
                var newOrder = new OrderWithId
                {
                    MyOrderViewModel = item,
                    Id = item.Id
                };
                listOrdersVM.Orders.Add(newOrder);
            }
            return View(listOrdersVM);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOrder(int orderId)
        {
            await _orderRepository.DeliveryOrder(orderId);
            return RedirectToAction("AllOrders", "Order");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RequestTable(TableRequest request)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.TableRepository.AddRequestAsync(request);
                await _unitOfWork.SaveAsync();
                return View("RequestComplete");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetRequests()
        {
            return View(_unitOfWork.TableRepository.GetRequests());
        }

        public async Task<IActionResult> CheckTable(int requestId, string status)
        {
            await _unitOfWork.TableRepository.CheckRequest(requestId, status);
            await _unitOfWork.SaveAsync();
            return View("GetRequests", _unitOfWork.TableRepository.GetRequests());
        }
    }
}