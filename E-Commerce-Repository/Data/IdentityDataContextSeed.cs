using E_Commerce_Core.Enities;
using E_Commerce_Core.Enities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Data
{
    public class IdentityDataContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName="AhmedTalaat",
                    Email="ahmedTalaat@gmail.com",
                    DisplayName="Ahmed Talaat",
                    
                    Address=new Address
                    {
                        City="Alexandria",
                        Country="Egypt",
                        PostalCode="1001",
                        State="Miami",
                        Street="6st"
                    }

                };

                await userManager.CreateAsync(user, "P@ssw0rd12345");
            }

          
            


        }

    }
}
