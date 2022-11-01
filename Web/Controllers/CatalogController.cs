using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Web.Models;
using Web.ViewModels.CatalogViewModels;

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

        public async Task<IActionResult> Index(int? brandSelected, int? categorySelected, int? productTypeSelected, int page, bool filterApplied)
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                // If whatever is to the left is null, use what's to the rig§ht.
                BrandSelected = brandSelected ?? 0,
                CategorySelected = categorySelected ?? 0,
                ProductTypeSelected = productTypeSelected ?? 0,
                Products = await _services.GetProducts(productTypeSelected, brandSelected, filterApplied, page),
                ProductType = await _services.GetAllProductTypes(),
                Brand = await _services.GetAllBrands(),
                Category = await _services.GetAllCategories(),
            };

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