using E_Commerce.API.Errors;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> Get(string id) {

            var basket = await _basketService.GetBasketAsync(id);

            return basket is null ? NotFound(new ApiResponse(404, $"Basket of id {id} Not Found")) : Ok(basket);
        
        }


        [HttpPost]

        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _basketService.UpdateBasketAsync(basketDto);

            return basket is null? NotFound(new ApiResponse(404, $"Basket of id {basketDto.Id} Not Found")) : Ok(basket);
        }

        [HttpDelete]

        public async Task<ActionResult> Delete(string id) => Ok(await _basketService.DeleteBasketAsync(id));


    }
}
