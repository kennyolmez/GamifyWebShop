using ApplicationCore.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
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

        public async Task<IActionResult> Index(string? searchString, int? page)
        {
            int totalFilteredProductCount = await _services.GetProductCount(null, null, searchString);
            var paginatedProducts = await _services.GetPaginatedProducts(page ?? 1, PagingUtilities.PageSize);

            IndexViewModel viewModel = new()
            {
                Products = paginatedProducts,
                TotalProductCount = totalFilteredProductCount,
                Page = page ?? 1,
            };

            return View(viewModel);
        }


        [HttpGet("/Product/{productId}")]
        public async Task<IActionResult> Product(int? productId)
        {
            IndexViewModel vm = new()
            {
                Product = await _services.GetProductById(productId)
            };

            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult Product(IndexViewModel vm)
        {
            return RedirectToAction("Product", new { productId = vm.ProductId });
        }

        [HttpPost]
        public IActionResult Brand(NavbarCategoryModel model, int? page)
        {
            return RedirectToAction("Brand", new {brandId = model.BrandId, page = page});
        }

        [HttpGet("/Brand/{brandId}")]
        public async Task<IActionResult> Brand(int brandId, int? page)
        {
            int totalFilteredProductCount = await _services.GetProductCount(null, brandId, null);
            var filteredAndPaginatedProducts = await _services.GetProducts(null, brandId, null, page ?? 1, PagingUtilities.PageSize);

            IndexViewModel vm = new()
            {
                BrandSelected = brandId,
                Products = filteredAndPaginatedProducts,
                Page = page ?? 1,
                TotalProductCount = totalFilteredProductCount,
            };

            return View("Index", vm);
        }

        [HttpPost]
        public IActionResult Category(NavbarCategoryModel model, int? page)
        {
            return RedirectToAction("Category", new { productTypeId = model.ProductTypeId, page = page });
        }

        [HttpGet("/Category/{productTypeId}")]
        public async Task<IActionResult> Category(int productTypeId, int? page)
        {
            int totalFilteredProductCount = await _services.GetProductCount(productTypeId, null, null);
            var filteredAndPaginatedProducts = await _services.GetProducts(productTypeId, null, null, page ?? 1, PagingUtilities.PageSize);

            IndexViewModel vm = new()
            {
                ProductTypeSelected = productTypeId,
                Products = filteredAndPaginatedProducts,
                Page = page ?? 1,
                TotalProductCount = totalFilteredProductCount,
            };
            return View("Index", vm);
        }

        [HttpPost]
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