using E_Commerce_Core.Enities.Identity;
using E_Commerce_Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extensions
{
    public static class DbInitializer
    {

        public async static Task InitializeDbAsync(WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<DataContext>();
                    var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();


                    //create DB if it dosen't exist
                    if ((await context.Database.GetPendingMigrationsAsync()).Any())

                        await context.Database.MigrateAsync();

                    //seed data
                    await DataContextSeed.SeedDataAsync(context);

                    //Seed Users
                    await IdentityDataContextSeed.SeedUserAsync(userManager);

                }
                catch (Exception ex)
                {

                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }

            }



        }
    }
}
