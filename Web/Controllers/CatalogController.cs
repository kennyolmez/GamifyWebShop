using ApplicationCore.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Web.Extensions;
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


        public async Task<IActionResult> Index(int? productTypeSelected, int? brandSelected, int? productId, string? searchString, int? page)
        {
            int productCount = await _services.GetProductCount(productTypeSelected, brandSelected, searchString);
            var filteredProducts = await _services.GetProducts(productTypeSelected, brandSelected, searchString, page ?? 1, PagingUtilities.PageSize);


            IndexViewModel viewModel = new IndexViewModel
            {
                Product = await _services.GetProductById(productId),
                Products = filteredProducts,
                ProductType = await _services.GetAllProductTypes(),
                Brand = await _services.GetAllBrands(),
                Category = await _services.GetAllCategories(),
                PaginationHelper = new PaginationHelper
                {
                    Page = page ?? 1,
                    ProductsOnPage = filteredProducts.Count(), // For view
                    ProductCount = productCount, // For view
                    PageCount = (int)Math.Ceiling(((decimal)productCount / PagingUtilities.PageSize))
                }
            };

            viewModel.PaginationHelper.NextIsEnabled = ((page ?? 1) < viewModel.PaginationHelper.PageCount) ? true : false; 
            viewModel.PaginationHelper.PreviousIsEnabled = (page > 1) ? true : false;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel vm, int? page)
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

        [HttpPost]
        // Doesn't have to be async since it's only a redirect, will use a thread if traffic is too high
        public IActionResult SearchCatalog(string? searchString)
        {
            return RedirectToAction("Index", new { searchString = searchString });
        }


        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}