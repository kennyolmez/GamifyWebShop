using ApplicationCore.DTOs;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Components
{
    public class NavbarCategoriesViewComponent : ViewComponent
    {
        private readonly CatalogServices _catalogSvc;
        public NavbarCategoriesViewComponent(CatalogServices catalogSvc)
        {
            _catalogSvc = catalogSvc;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            NavbarCategoryModel model = new()
            {
                ProductTypes = await _catalogSvc.GetAllProductTypes(),
                Brands = await _catalogSvc.GetAllBrands(),
                Categories = await _catalogSvc.GetAllCategories(),
            };

            return View(model);
        }

    }
}
