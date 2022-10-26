using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (dbContext.Database.IsSqlServer())
                {
                    // Create migration
                    if (dbContext.Database.IsSqlServer())
                    {
                        dbContext.Database.Migrate();
                    }

                    if (!await dbContext.Brands.AnyAsync())
                    {
                        await dbContext.Brands.AddRangeAsync(GetBrandSeed());

                        await dbContext.SaveChangesAsync();
                    }

                    if (!await dbContext.Categories.AnyAsync())
                    {
                        await dbContext.Categories.AddRangeAsync(GetCategorySeed());

                        await dbContext.SaveChangesAsync();
                    }

                    if (!await dbContext.Products.AnyAsync())
                    {
                        await dbContext.Products.AddRangeAsync(GetProductSeed().ToList());

                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(dbContext, logger, retryForAvailability);
                throw;
            }



        }

        // Creates and returns a list of brands
        public static IEnumerable<Brand> GetBrandSeed()
        {
            return new List<Brand>
            {
                new("Acer"),
                new("Lethal Gaming Gear"),
                new("ZOWIE BY BENQ"),
                new("LG"),
                new("Logitech")
            };
        }

        public static IEnumerable<Category> GetCategorySeed()
        {
            return new List<Category>
            {
                new("Monitor"),
                new("Mouse"),
                new("Mousepad"),
            };
        }

        public static IEnumerable<Product> GetProductSeed()
        {
            return new List<Product>
            {
                new("XL2411K 24 1080p 144hz Gaming Monitor with DyAc", "Great monitor by Zowie", "https://www.maxgaming.se/bilder/artiklar/zoom/17029_1.jpg?m=1608736124", 2989, 3, 1, 10),
                new("XL2566K 24.5 TN 360Hz DyAc+ Gaming Monitor For Esports", "Another great monitor by Zowie", "https://www.maxgaming.se/bilder/artiklar/22371.jpg?m=1662712181", 8989, 3, 1, 5),
                new("G PRO X Superlight Wireless Gaming Mouse Black", "Gaming mouse by Logitech", "https://www.maxgaming.se/bilder/artiklar/zoom/17200_1.jpg?m=1605689114", 1589, 5, 2, 55),
                new("G703 Lightspeed Hero Wireless Gaming Mouse", "Lightspeed wireless gaming mouse by Logitech", "https://www.tradeinn.com/f/13785/137855799_7/logitech-tradlos-mus-g703-lightspeed.jpg", 989, 5, 2, 55),
                new("Saturn Gaming Mousepad XL", "Carefully crafted mousepad by LGG", "https://www.maxgaming.se/bilder/artiklar/zoom/21236_1.jpg?m=1647359517", 379, 2, 3, 55),
                new("Venus Gaming Mousepad XL", "Carefully crafted mousepad by LGG", "https://www.maxgaming.se/bilder/artiklar/zoom/21241_1.jpg?m=1647359594", 379, 2, 3, 55),
            };
        }
    }
}
