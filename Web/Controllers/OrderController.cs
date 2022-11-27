using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Web.Extensions;
using Web.ViewModels.OrderViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderServices _orderServices;
        private readonly CartServices _cartServices;
        private readonly Lazy<string> _userId;

        public OrderController(OrderServices orderServices, CartServices cartServices, ILogger<OrderController> logger)
        {
            _orderServices = orderServices;
            _cartServices = cartServices;
            _logger = logger;
            _userId = new(() => HttpContext.GetUserId());
        }

        // Better to fetch directly from services instead of passing in a parameter, as that may jeopardize app security
        // For example, somebody could get your cart Id and check it out if this were a Post
        public async Task<IActionResult> CreateOrder()
        {
            OrderViewModel vm = new OrderViewModel
            {
                UserCart = await _cartServices.GetOrCreateCart(_userId.Value)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            var cart = await _cartServices.GetOrCreateCart(_userId.Value);

            await _orderServices.CreateOrder(_userId.Value,
                                             cart.Id,
                                             model.EmailAddress,
                                             model.FirstName,
                                             model.LastName,
                                             model.PhoneNumber,
                                             model.ZipCode,
                                             model.StreetAddress,
                                             model.City,
                                             model.County,
                                             model.AddressName);

            return View();
        }
    }
}
