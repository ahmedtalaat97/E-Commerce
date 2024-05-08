using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities;
using E_Commerce_Core.Enities.OrderEntities;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Core.Interfaces.Services;
using E_Commerce_Repository.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketService basketService, IPaymentService paymentService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _paymentService = paymentService;
        }

        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            //1- get basket 

            var basket = await _basketService.GetBasketAsync(input.BasketId);

            //2- create order item list and get order items from basket items

            if (basket is null) throw new Exception($"No basket with ID {input.BasketId}");

            var orderItems = new List<OrderItem>();

            foreach (var BasketItem in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Product, int>().GetAsync(BasketItem.ProductId);
                if (product is null) continue;

                var productItem = new OrderItemProduct
                {
                    PictureUrl = product.PictureUrl,
                    ProductId = product.Id,
                    ProductName = product.Name,
                };


                var orderItem = new OrderItem
                {
                    OrderItemProduct = productItem,
                    Price = product.Price,
                    Quantity = BasketItem.Quantity

                };

                orderItems.Add(orderItem);

            }

            if (!orderItems.Any()) throw new Exception("No Basket Items was found");

            //3- Delivery Method
            if (!input.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method was Selected");
            
            var delivery= await _unitOfWork.Repository<DeliveryMethod,int>().GetAsync(input.DeliveryMethodId.Value);

            if (delivery is null) throw new Exception("No Delivery Method Was found");

            //4- Shipping Adress

            var shippingAddress = _mapper.Map<ShippingAdress>(input.ShippingAdress);

            // 5- Calculate Sub Total

            //TODO Create Payment Intent For This Order

            var spec = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);
            if (existingOrder is not null)
            {
                _unitOfWork.Repository<Order, Guid>().Delete(existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);

            }
            else
            {
               basket= await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(basket.Id);

            }


            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 6- map orderitemDto to OrderItem

            var mappedItems=_mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            {
                Email = input.Email,
                ShippingAdress = shippingAddress,
                DeliveryMethod=delivery,
                OrderItems = mappedItems,
                SubTotal= subTotal,
                BasketId=basket.Id,
                PaymentIntentId=basket.PaymentIntentId
                
            };

            await _unitOfWork.Repository<Order,Guid>().AddAsync(order);

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetAllOrderAsync(string email)
        {
           var spec= new OrderSpecifications(email);

            var order= await _unitOfWork.Repository<Order,Guid>().GetAllWithSpecAsync(spec);

            if (!order.Any()) throw new Exception($"no orders for user {email}");

            return _mapper.Map<IEnumerable<OrderResultDto>>(order);

        }

        
        public async Task<OrderResultDto> GetOrderAsync(Guid id, string email)
        {
            var spec = new OrderSpecifications(email);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);

            if (order is null) throw new Exception($"no orders for user {email}");
            return _mapper.Map<OrderResultDto>(order);


        }
        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()=>await _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();
    }
}
