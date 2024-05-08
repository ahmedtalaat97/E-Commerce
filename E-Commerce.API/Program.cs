using E_Commerce.API.Errors;
using E_Commerce.API.Extensions;
using E_Commerce.API.Helper;
using E_Commerce_Core.Enities.Identity;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Core.Interfaces.Services;
using E_Commerce_Repository.Data;
using E_Commerce_Repository.Repositories;
using E_Commerce_Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(o=>o.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));


            builder.Services.AddDbContext<IdentityDataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySQLConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerService();

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddIdentityService(builder.Configuration);

            var app = builder.Build();
            await  DbInitializer.InitializeDbAsync(app);

            app.UseMiddleware<CustomExceptionHandler>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            
            app.UseAuthentication();
            app.UseAuthorization();
            


            app.MapControllers();

           

            app.Run();
        }
   
    
       
    
//       {
//  "email": "ahmedTalaat@gmail.com",
//  "password": "P@ssw0rd12345"
//}




}



    

}
