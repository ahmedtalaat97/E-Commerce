using E_Commerce_Core.Enities.Basket;
using E_Commerce_Core.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database= connection.GetDatabase();
        }

        public async Task<bool> DeleteCustomerBasketAsync(string id) => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string id)
        {
            var basket= await _database.StringGetAsync(id);
           return basket.IsNullOrEmpty?null :JsonSerializer.Deserialize<CustomerBasket>(basket);

            
        }

        public async Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket)
        {
           var serializedBasket=JsonSerializer.Serialize(basket);

            var result= await _database.StringSetAsync(basket.Id, serializedBasket, TimeSpan.FromDays(7));

            return result ? await GetCustomerBasketAsync(basket.Id) : null;
          



        }
    }
}
