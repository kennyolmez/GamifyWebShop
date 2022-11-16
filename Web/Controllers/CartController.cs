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
            string? userId = GetUserOrCreateCookie();


            IndexViewModel vm = new IndexViewModel
            {
                UserCart = await _cartServices.GetOrCreateCart(userId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = GetUserOrCreateCookie();

            var cart = await _cartServices.GetOrCreateCart(userId);

            await _cartServices.AddToCart(userId, productId, cart.Id);

            return RedirectToAction("Index", "Catalog");
        }

        // Should make a middleware of this to avoid bloating the controller
        public string GetUserOrCreateCookie()
        {
            string? userName = null;

            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            if (Request.Cookies.ContainsKey("first_request"))
            {
                userName = Request.Cookies["first_request"];

                if (!Request.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!Guid.TryParse(userName, out var _))
                    {
                        userName = null;
                    }
                }
            }

            if (userName != null) return userName;

            userName = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true };
            Response.Cookies.Append("first_request", userName, cookieOptions);

            return userName;
        }
    }
}
