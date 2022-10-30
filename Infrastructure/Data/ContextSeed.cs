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
                    // Create migration
                    if (dbContext.Database.IsSqlServer()) 
                    {
                        dbContext.Database.Migrate();
                    }

                    if (!await dbContext.Brands.AnyAsync()) // Async because goes through the database
                    {
                        dbContext.Brands.AddRange(GetBrandSeed()); // Not async because AddAsync is only used when you have custom EF value generators

                        await dbContext.SaveChangesAsync(); // Async because hits the DB
                    }

                    if (!await dbContext.Categories.AnyAsync())
                    {
                        dbContext.Categories.AddRange(GetCategorySeed());

                        await dbContext.SaveChangesAsync();
                    }

                    if(!await dbContext.ProductTypes.AnyAsync())
                    {
                         dbContext.ProductTypes.AddRange(GetProductTypeSeed());

                         await dbContext.SaveChangesAsync();
                    }

                    if (!await dbContext.Products.AnyAsync())
                    {
                        dbContext.Products.AddRange(GetProductSeed()); 

                        await dbContext.SaveChangesAsync();
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
                new("Logitech"),
                new("DXRACER"),
                new("Microsoft")
            };
        }

        public static IEnumerable<Category> GetCategorySeed()
        {
            return new List<Category>
            {
                new("PC Peripherals"),
                new("PC Parts"),
                new("Console"),
                new("Gaming Chair")
            };
        }

        public static IEnumerable<ProductType> GetProductTypeSeed()
        {
            return new List<ProductType>
            {
                new("Monitor", 1),
                new("Mouse", 1),
                new("Mousepad", 1),
                new("Console", 3),
                new("Controller", 3),
                new("GPU", 2),
                new("CPU", 2),
                new("Prince Series", 4)
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
                new("PRINCE P08-NW", "Gaming Chair by DXRACER", "https://www.maxgaming.se/bilder/artiklar/38015.jpg?m=1626676331", 2990, 6, 8, 5),
                new("XBOX Series X", "Console by MICROSOFT", "https://www.elgiganten.se/image/dv_web_D180001002520756/218667/xbox-series-x-1-tb-svart.jpg", 2990, 7, 4, 50),
            };
        }
    }
}
