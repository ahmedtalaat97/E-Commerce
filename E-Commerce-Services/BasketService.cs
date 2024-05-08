using AutoMapper;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities.Basket;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string id) => await _repository.DeleteCustomerBasketAsync(id);

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
         var basket=    await _repository.GetCustomerBasketAsync(id);

            return basket is null ? null: _mapper.Map<BasketDto?>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket= _mapper.Map<CustomerBasket>(basket);

            var updateCustomerBasket= await _repository.UpdateCustomerBasketAsync(customerBasket);

            return updateCustomerBasket is null ? null : _mapper.Map<BasketDto>(updateCustomerBasket);



        }
    }
}
