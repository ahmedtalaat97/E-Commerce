using E_Commerce_Core.Enities;
using E_Commerce_Core.Enities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Data
{
    public static class DataContextSeed
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            if (!context.Set<ProductBrand>().Any())
            {
                //1- Read Data From Files
                var brandsData = await File.ReadAllTextAsync(@"..\E-Commerce-Repository\Data\DataSeeding\brands.json");
                //2- Convert Data to C# objects
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                //3- Insert Data into database
                if ( brands is not null && brands.Any() )
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brands);
                  await  context.SaveChangesAsync();

                }


            }




            if (!context.Set<ProductType>().Any())
            {
                //1- Read Data From Files
                var typesData = await File.ReadAllTextAsync(@"..\E-Commerce-Repository\Data\DataSeeding\types.json");
                //2- Convert Data to C# objects
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                //3- Insert Data into database
                if (types is not null && types.Any()  )
                {
                    await context.Set<ProductType>().AddRangeAsync(types);
                    await context.SaveChangesAsync();

                }


            }



            if (!context.Set<Product>().Any())
            {
                //1- Read Data From Files
                var productsData = await File.ReadAllTextAsync(@"..\E-Commerce-Repository\Data\DataSeeding\products.json");
                //2- Convert Data to C# objects
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                //3- Insert Data into database
                if (  products is not null && products.Any())
                {
                    await context.Set<Product>().AddRangeAsync(products);
                    await context.SaveChangesAsync();

                }


            }



            if (!context.Set<DeliveryMethod>().Any())
            {
                //1- Read Data From Files
                var methodData = await File.ReadAllTextAsync(@"..\E-Commerce-Repository\Data\DataSeeding\delivery.json");
                //2- Convert Data to C# objects
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(methodData);

                //3- Insert Data into database
                if (methods is not null && methods.Any())
                {
                    await context.Set<DeliveryMethod>().AddRangeAsync(methods);
                    await context.SaveChangesAsync();

                }


            }
        }
    }
}

//D:\ASP.Net-Course\API-Project\E-Commerce\E-Commerce-Repository\Data\DataSeeding\delivery.json