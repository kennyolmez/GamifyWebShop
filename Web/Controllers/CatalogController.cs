using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet]
        public async Task<IActionResult> Index(int? categorySelected, int? productTypeSelected, int? brandSelected, int? productId, int? page)
        {
            int pageSize = 3; // Page size, temporary. Not sure where to put this.
            int totalProductCount = (await _services.GetAllProducts()).Count();

            IndexViewModel viewModel = new IndexViewModel
            {
                Product = await _services.GetProductById(productId),
                Products = await _services.GetProducts(productTypeSelected, brandSelected, page ?? 1, pageSize),
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

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel vm, int? page)
        {
            // This is where we do validation

            return RedirectToAction("Index",
                new { categorySelected = vm.CategorySelected, 
                    productTypeSelected = vm.ProductTypeSelected, 
                    brandSelected = vm.BrandSelected, 
                    productId = vm.ProductId,
                    page = page,
                });
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}