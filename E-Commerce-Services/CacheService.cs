using E_Commerce_Core.Interfaces.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce_Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetCacheResponseAsync(string key)
        {
          var response= await _database.StringGetAsync(key);
            return response.IsNullOrEmpty? null : response.ToString();
        }

        public async Task SetCacheResponseAsync(string key, object response, TimeSpan time)
        {
            var serializedResponse=JsonSerializer.Serialize(response ,new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase });
            await _database.StringSetAsync(key, serializedResponse, time);
            
        }
    }
}
