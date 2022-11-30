using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using Web.ViewModels.CartViewModels;
using Microsoft.AspNetCore.Http;
using Web.Extensions;


namespace Web.Controllers
{
    public class CartController : Controller
    {

        private readonly CartServices _cartServices;
        private readonly Lazy<string> _userId;
        public CartController(CartServices cartServices)
        {
            _cartServices = cartServices;
            // Defers the initialization of GetUserId until the Value property (which is a method) is called
            _userId = new(() => HttpContext.GetUserId()); 
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartServices.GetOrCreateCart(_userId.Value);

            IndexViewModel vm = new IndexViewModel
            {
                UserCart = cart
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            await _cartServices.GetOrCreateCart(_userId.Value);

            await _cartServices.AddToCart(_userId.Value, productId);

            return RedirectToAction("Index", "Catalog");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(Dictionary<int, int> productAndQuantity)
        {
            if(productAndQuantity.Values.Any(x => x < 0))
            {
                TempData["NegativeQuantityError"] = "Quantity cannot be negative";

                return RedirectToAction("Index");
            }

            await _cartServices.UpdateQuantity(productAndQuantity);

            return RedirectToAction("Index");
        }
    }
}
