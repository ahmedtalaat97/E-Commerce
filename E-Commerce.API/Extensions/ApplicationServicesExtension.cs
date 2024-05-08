using E_Commerce.API.Errors;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Core.Interfaces.Services;
using E_Commerce_Repository.Repositories;
using E_Commerce_Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheService, CacheService>();
      

            //builder.Services.AddAutoMapper(map => map.AddProfile(new MappingProfile()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddSingleton<IConnectionMultiplexer>(options =>

            {

                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));

                return ConnectionMultiplexer.Connect(config);


            }
            );


            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = context =>
                {

                    var errors = context.ModelState.Where(m => m.Value.Errors.Any()).SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage).ToList();


                    return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
                };
            }
            );
            return services;
        }
    }
}
