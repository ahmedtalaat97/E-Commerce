using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();

        public Task<OrderResultDto> CreateOrderAsync(OrderDto input);

        public Task<OrderResultDto> GetOrderAsync(Guid id,string email);

        public Task<IEnumerable<OrderResultDto>> GetAllOrderAsync(string email);
    }
}
