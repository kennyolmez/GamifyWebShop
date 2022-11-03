using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Web.Models;
using Web.ViewModels.CatalogViewModels;
using Web.ViewModels.Pagination;

namespace Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly CatalogServices _services; // Rename to catalog services

        public CatalogController(ILogger<CatalogController> logger, CatalogServices services)
        {
            _logger = logger;
            _services = services;
        }

        public async Task<IActionResult> Index(int? brandSelected, int? categorySelected, int? productTypeSelected, int? page, bool filterApplied)
        {
            int pageSize = 2; // Page size, temporary. Not sure where to put this.
            int totalProductCount = (await _services.GetAllProducts()).Count();

            IndexViewModel viewModel = new IndexViewModel
            {
                // If whatever is to the left is null, use what's to the right.
                BrandSelected = brandSelected ?? 0,
                CategorySelected = categorySelected ?? 0,
                ProductTypeSelected = productTypeSelected ?? 0,
                Products = await _services.GetProducts(productTypeSelected, brandSelected, filterApplied, page ?? 1, pageSize),
                ProductType = await _services.GetAllProductTypes(),
                Brand = await _services.GetAllBrands(),
                Category = await _services.GetAllCategories(),
                PaginationHelper = new PaginationHelper()
                {
                    Page = page ?? 1,
                    ProductCount = totalProductCount,
                    PageCount = (int)Math.Ceiling(((decimal)totalProductCount / pageSize)),
                }
            };

            viewModel.PaginationHelper.NextIsEnabled = ((page ?? 1) < viewModel.PaginationHelper.PageCount) ? true : false; 
            viewModel.PaginationHelper.PreviousIsEnabled = (page > 1) ? true : false;

            return View(viewModel);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}