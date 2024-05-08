using AutoMapper;
using AutoMapper.Execution;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;
using E_Commerce_Core.Enities.OrderEntities;

namespace E_Commerce.API.Helper
{
    public class OrderItemResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
            =>
            !string.IsNullOrWhiteSpace(source.OrderItemProduct.PictureUrl) ? $"{_configuration["BaseUrl"]}{source.OrderItemProduct.PictureUrl}" : string.Empty;
    }
}
