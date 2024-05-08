using E_Commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {

        private readonly int _time;

        public CacheAttribute(int time)
        {
            _time = time;
        }

    
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheKey = GenerateKeyFromRequest(context.HttpContext.Request);

            var cacheService= context.HttpContext.RequestServices.GetService<ICacheService>();
            


            var cacheResponse=  await cacheService.GetCacheResponseAsync(cacheKey);

            if (cacheResponse != null)
            {
                var result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = 200,
                    Content = cacheResponse
                };
                context.Result= result;
                return;
            }

           var excutedContext= await next();
            if ( excutedContext.Result is OkObjectResult response)
            await cacheService.SetCacheResponseAsync(cacheKey, response.Value, TimeSpan.FromHours(_time));
        }

        private string GenerateKeyFromRequest(HttpRequest request) 
        {
            StringBuilder key = new StringBuilder();

            key.Append($"{request.Path}");

            foreach (var item in request.Query.OrderBy(x=>x.Key))
            {
                key.Append($"{item}");

            }
            return key.ToString();
        
        }
    }
}
