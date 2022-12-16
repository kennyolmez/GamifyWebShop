using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Web.ViewModels.Pagination;

namespace Web.Components
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int productCount, int productsOnPage, int? page)
        {
            PaginationHelper model = new()
            {
                Page = page ?? 1,
                ProductsOnPage = productsOnPage,
                ProductCount = productCount, // For view
                PageCount = (int)Math.Ceiling(((decimal)productCount / PagingUtilities.PageSize))
            };


            model.NextIsEnabled = ((page ?? 1) < model.PageCount) ? true : false;
            model.PreviousIsEnabled = (page > 1) ? true : false;

            return View(model);
        }
    }
}
