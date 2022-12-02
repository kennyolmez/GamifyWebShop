using ApplicationCore.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Extensions;
using Web.ViewModels.CatalogViewModels;
using Web.ViewModels.Pagination;
using Web.ViewModels.Validators;

namespace Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly CatalogServices _catalogService;
        private readonly Lazy<string> _userId;
        private readonly IValidator<_ProductViewModel> _reviewValidator;
        public ReviewController(CatalogServices catalogService, IValidator<_ProductViewModel> reviewValidator)
        {
            _catalogService = catalogService;
            _userId = new(() => HttpContext.GetUserId());
            _reviewValidator = reviewValidator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostReview(_ProductViewModel model)
        {
            ValidationResult result = await _reviewValidator.ValidateAsync(model);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid)
            {
                await _catalogService.AddReviewToCatalogProduct(model.Comment, model.Rating, _userId.Value, User.Identity.Name, model.ProductId);

                return RedirectToAction("Index", "Catalog", new { productId = model.ProductId });
            }

            // Create mapping between ViewModels here
            // Makes this action kind of bloated?
            // Need this viewmodel to render the right view.
            IndexViewModel viewModel = new IndexViewModel
            {
                Product = await _catalogService.GetProductById(model.ProductId),
                ProductType = await _catalogService.GetAllProductTypes(),
                Brand = await _catalogService.GetAllBrands(),
                Category = await _catalogService.GetAllCategories(),
            };

            return View("Views/Catalog/Index.cshtml", viewModel);
        }
    }
}
