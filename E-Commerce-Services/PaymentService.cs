using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = E_Commerce_Core.Enities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Core.Enities.OrderEntities;
using E_Commerce_Repository.Specifications;

namespace E_Commerce_Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper,
            IBasketService basketService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _basketService = basketService;
            _configuration = configuration;
        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket)
        {
            //1- Calculate Total Price
            //1.1=> Price* Quantity 

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            foreach (var item in basket.BasketItems)
            {
                //Check Product Price

                var product= await _unitOfWork.Repository<Product,int>().GetAsync(item.ProductId);

                if (product.Price!= item.Price)
                    item.Price= product.Price;


            }

            var total= basket.BasketItems.Sum(item=> (item.Price) *item.Quantity);

            //1.3=>Shipping Price

            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Was Selected");

            var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod,int>().GetAsync(basket.DeliveryMethodId.Value);

            var shippingPrice = deliveryMethod.Price;
            basket.ShippingPrice= shippingPrice;

            //Calculate Total Amount in the Smallest Unit 

            long amount =(long) ((total*100) +(shippingPrice*100));

            // 2 - Create Or Update

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                //Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

                //service.CreateAsync()
            }
            else
            {
                //Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                   
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);

            }
           await _basketService.UpdateBasketAsync(basket);

            return basket;

        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {
            //0 get basket by id

            var basket = await _basketService.GetBasketAsync(basketId);

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            foreach (var item in basket.BasketItems)
            {
                //Check Product Price

                var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.ProductId);

                if (product.Price != item.Price)
                    item.Price = product.Price;


            }

            var total = basket.BasketItems.Sum(item => (item.Price) * item.Quantity);

            //1.3=>Shipping Price

            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Was Selected");

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);

            var shippingPrice = deliveryMethod.Price;
            basket.ShippingPrice = shippingPrice;

            //Calculate Total Amount in the Smallest Unit 

            long amount = (long)((total * 100) + (shippingPrice * 100));

            // 2 - Create Or Update

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                //Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

                //service.CreateAsync()
            }
            else
            {
                //Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,

                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            await _basketService.UpdateBasketAsync(basket);

            return basket;
        }



        public async Task<OrderResultDto> UpdatePaymentStatusFailed(string PaymentIntentId)
        {

            // 1- Get order by payment intent id => new sepc class
            var spec = new OrderWithPaymentIntentIdSpecifications(PaymentIntentId);
            var order = await _unitOfWork.Repository<Order,Guid>().GetWithSpecAsync(spec);
            if (order is null) throw new Exception($"No Order with PaymentIntentId {PaymentIntentId}");
            
            //2. update payment status
            
            order.PaymentStatus= PaymentStatus.Failed;

          _unitOfWork.Repository<Order,Guid>().Update(order);

            // 3 - Save Changes
           await  _unitOfWork.CompleteAsync();

            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<OrderResultDto> UpdatePaymentStatusSucceeded(string PaymentIntentId)
        {
            // 1- Get order by payment intent id => new sepc class
            var spec = new OrderWithPaymentIntentIdSpecifications(PaymentIntentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecAsync(spec);
            if (order is null) throw new Exception($"No Order with PaymentIntentId {PaymentIntentId}");

            //2. update payment status

            order.PaymentStatus = PaymentStatus.Recived;

            _unitOfWork.Repository<Order, Guid>().Update(order);

            // 3 - Save Changes
            await _unitOfWork.CompleteAsync();

            // 4- delete Customer Basked

            await _basketService.DeleteBasketAsync(order.BasketId);

            return _mapper.Map<OrderResultDto>(order);
        }
    }
}
