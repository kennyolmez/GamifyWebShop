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
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : Request.Cookies["guest"];
            var cart = await _cartServices.GetOrCreateCart(userId);


            IndexViewModel vm = new IndexViewModel
            {
                UserCart = cart
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : Request.Cookies["guest"];
            await _cartServices.GetOrCreateCart(userId);

            await _cartServices.AddToCart(userId, productId);

            return RedirectToAction("Index", "Catalog");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(Dictionary<int, int> productAndQuantity)
        {
            await _cartServices.UpdateQuantity(productAndQuantity);

            return RedirectToAction("Index");
        }
    }
}
