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
                .Include(x => x.CartProducts)
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
        public async Task AddToCart(string? userId, int productId)
        {
            var cart = await _context.ShoppingCarts.Where(x => x.BuyerId == userId).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new ShoppingCart(userId);
                _context.ShoppingCarts.Add(cart);
            }

            var product = await _context.Products.Include(x => x.Brand).Where(x => x.Id == productId).FirstOrDefaultAsync(); 

            cart.AddItem(product, 1);

            _context.SaveChanges();
        }

        public async Task UpdateQuantity(Dictionary<int, int> productAndQuantity)
        {
            var cartItems = await _context.ShoppingCartItems.Where(x => productAndQuantity.Keys.Any(p => p == x.Id)).Include(x => x.ShoppingCart).ToListAsync();

            foreach (var cartItem in cartItems)
            {
                foreach (var p in productAndQuantity)
                {
                    if (p.Key == cartItem.Id)
                    {
                        cartItem.SetQuantity(p.Value);
                    }
                }

                cartItem.ShoppingCart.RemoveEmptyItems();
            }

            _context.SaveChanges();
        }
    }
}
