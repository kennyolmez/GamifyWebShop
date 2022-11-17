using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using Web.ViewModels.CartViewModels;
using Microsoft.AspNetCore.Http;


namespace Web.Controllers
{
    public class CartController : Controller
    {

        private readonly CartServices _cartServices;
        public CartController(CartServices cartServices)
        {
            _cartServices = cartServices;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : Request.Cookies["first_request"];

            IndexViewModel vm = new IndexViewModel
            {
                UserCart = await _cartServices.GetOrCreateCart(userId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : Request.Cookies["first_request"];

            var cart = await _cartServices.GetOrCreateCart(userId);

            await _cartServices.AddToCart(userId, productId, cart.Id);

            return RedirectToAction("Index", "Catalog");
        }
    }
}
