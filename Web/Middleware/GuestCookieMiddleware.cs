using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Globalization;
using System.Security.Claims;

namespace Web.Middleware
{
    public class GuestCookieMiddleware
    {
        private readonly RequestDelegate _next;
        public GuestCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? userName = null;


            if (context.Request.Cookies.ContainsKey("guest"))
            { // If guest has a cookie

                userName = context.Request.Cookies["guest"];

                if (!context.User.Identity.IsAuthenticated)
                {
                    if (!Guid.TryParse(userName, out var _))
                    {
                        userName = null;
                    }
                }

            }

            if (userName == null)
            {
                userName = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(3) };
                context.Response.Cookies.Append("guest", userName, cookieOptions);
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class GuestCookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseGuestCookie(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GuestCookieMiddleware>();
        }
    }
}

