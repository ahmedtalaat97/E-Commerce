using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace E_Commerce.API.Extensions
{
    public static class SwaggerExtensions
    {

        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen(options =>
            //{
            //    var scheme= new OpenApiSecurityScheme
            //    {
            //        Description= "Standard Authorization header using the beraer scheme , e.g\"bearer{token}\"",
            //        In = ParameterLocation.Header,
            //        Name="Authorization",
            //        Type=SecuritySchemeType.ApiKey

            //    };

            //    options.AddSecurityDefinition("bearer", scheme);
            //    options.OperationFilter<SecurityRequirementsOperationFilter>();
            //});

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            return services;
        }
    }
}
