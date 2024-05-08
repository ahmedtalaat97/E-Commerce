using AdminDashboard.Helpers;
using E_Commerce_Core.Enities.Identity;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Repository.Data;
using E_Commerce_Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace AdminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(o => 
            o.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
            builder.Services.AddDbContext<IdentityDataContext>(o
                => o.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySQLConnection")));







            #region Identity
      

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt=>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
            }
            
            ).AddEntityFrameworkStores<IdentityDataContext>();

            #endregion

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ProductMappingProfile));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}