using ApplicationCore.DTOs;
using ApplicationCore.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using Web.Extensions;
using Web.Models.ServicePointAggregate;
using Web.ViewModels.OrderViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderServices _orderServices;
        private readonly CartServices _cartServices;
        private readonly Lazy<string> _userId;
        private IValidator<CheckoutViewModel> _orderValidator;

        public OrderController(OrderServices orderServices, CartServices cartServices, ILogger<OrderController> logger, IValidator<CheckoutViewModel> orderValidator)
        {
            _orderServices = orderServices;
            _cartServices = cartServices;
            _logger = logger;
            _userId = new(() => HttpContext.GetUserId());
            _orderValidator = orderValidator;
        }

        // Better to fetch directly from services instead of passing in a parameter, as that may jeopardize app security
        // For example, somebody could get your cart Id and check it out if this were a Post
        public async Task<IActionResult> CreateOrder()
        {
            CheckoutViewModel vm = new CheckoutViewModel
            {
                UserCart = await _cartServices.GetOrCreateCart(_userId.Value)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var cart = await _cartServices.GetOrCreateCart(_userId.Value);

            ValidationResult result = await _orderValidator.ValidateAsync(model);
            result.AddToModelState(this.ModelState);

            if (ModelState.IsValid)
            {
                await _orderServices.CreateOrder(_userId.Value,
                                             cart.Id,
                                             model.EmailAddress,
                                             model.FirstName,
                                             model.LastName,
                                             model.PhoneNumber,
                                             model.PostalCode,
                                             model.StreetAddress,
                                             model.City,
                                             model.AddressName);

                return RedirectToAction("Index", "Catalog");
            }

            model.UserCart = await _cartServices.GetOrCreateCart(_userId.Value);

            return View("CreateOrder", model);
        }

        [HttpPost]
        public async Task<IActionResult> AutofillAddressInformation(string postalCode)
        {
            List<ServicePoint> servicePoints = await GetAddressInformation(postalCode);

            if(servicePoints is not null)
            {
                CheckoutViewModel vm = new CheckoutViewModel
                {
                    AddressName = servicePoints.First().Name,
                    StreetAddress = $"{servicePoints.First().DeliveryAddress.StreetName} {servicePoints.First().DeliveryAddress.StreetNumber}",
                    City = servicePoints.First().DeliveryAddress.City,
                    PostalCode = servicePoints.First().DeliveryAddress.PostalCode,
                };

                vm.UserCart = await _cartServices.GetOrCreateCart(_userId.Value);

                return View("CreateOrder", vm);
            }
            
            return RedirectToAction("CreateOrder");
        }

        public async Task<List<ServicePoint>> GetAddressInformation(string postalCode)
        {
            string baseURL = "https://atapi2.postnord.com/rest/businesslocation/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync($"https://atapi2.postnord.com/rest/businesslocation/v5/servicepoints/bypostalcode?apikey=c54fa0bc570d72bb3e45613f01f028a4&returnType=json&countryCode=SE&postalCode={postalCode}&context=optionalservicepoint&responseFilter=public");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    var addressInformation = JsonConvert.DeserializeObject<ServicePointInformationRoot>(results);

                    return addressInformation.ServicePointInformationResponse.ServicePoints;
                }
            }

            return null;
        }
    }
}
