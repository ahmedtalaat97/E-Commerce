using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities.OrderEntities;
using E_Commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> Create(OrderDto input)
        {
            var order=await _orderService.CreateOrderAsync(input);
            return Ok(order);

        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrders()
        {
            var email =User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrderAsync(email);

            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrder(Guid orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrderAsync(orderId, email);

            return Ok(orders);
        }


        [HttpGet("Delivery")]

        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods() => Ok(await _orderService.GetDeliveryMethodsAsync());

    }
}
