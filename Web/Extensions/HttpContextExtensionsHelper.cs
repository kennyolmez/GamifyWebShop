using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Web;

namespace Web.Extensions
{
    public static class HttpContextExtensionsHelper
    {
        public static string GetUserId(this HttpContext _context)
        {
            var userId = _context.User.Identity!.IsAuthenticated
            ?
                _context.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()
                :
                _context.Request.Cookies["guest"];

            return userId;
        }
    }
}
