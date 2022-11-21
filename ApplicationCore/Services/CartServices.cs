using ApplicationCore.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ApplicationCore.Services
{
    public class CartServices
    {
        private readonly ApplicationDbContext _context;

        public CartServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCartDto> GetOrCreateCart(string? userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(x => x.Products).ThenInclude(x => x.Brand)
                .Include(x => x.Products).ThenInclude(x => x.ProductType).ThenInclude(x => x.Category)
                .Where(x => x.BuyerId == userId).Select(x => new ShoppingCartDto(x))
                .FirstOrDefaultAsync();


            if (cart == null)
            {
                return await CreateCartForUser(userId);
            }

            return cart;
        }

        public async Task<ShoppingCartDto> CreateCartForUser(string? userId)
        {
            var cart = new ShoppingCart(userId);

            _context.ShoppingCarts.Add(cart);

            await _context.SaveChangesAsync();

            return await _context.ShoppingCarts.Where(x => x.BuyerId == userId).Select(x => new ShoppingCartDto(x)).FirstAsync();
        }
        
        // Add quantity and price too parameters?
        public async Task AddToCart(string? userId, int productId, int cartId)
        {
            var cart = await _context.ShoppingCarts.Include(x => x.Products).Where(x => x.BuyerId == userId).FirstOrDefaultAsync();

            if(cart == null)
            {
                cart = new ShoppingCart(userId);
                _context.ShoppingCarts.Add(cart);
            }

            // FirstAsync since we know the product should always exist. 
            cart.Products.Add(await _context.Products.Where(x => x.Id == productId).FirstAsync());

            _context.SaveChanges();
        }

        // Redundant for now
        //public async Task<List<ProductDto>> GetCartItems(string userId)
        //{
        //    var cart = await _context.ShoppingCarts
        //        .Include(x => x.Products).ThenInclude(x => x.Brand)
        //        .Include(x => x.Products).ThenInclude(x => x.ProductType).ThenInclude(x => x.Category)
        //        .Where(x => x.BuyerId == userId).FirstOrDefaultAsync();

            

        //    var cartItems = cart.Products.ToList();

        //    var cartItemsOutput = cartItems.Select(x => new ProductDto(x)).ToList();

        //    return cartItemsOutput;
        //}
    }
}
