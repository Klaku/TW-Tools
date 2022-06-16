using Microsoft.AspNetCore.Mvc;
using CoreApi.Models.DB;
using System.Collections.Generic;
using WebApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayerController : ApiController
    {
        public PlayerController(IMemoryCache _cache, IConfiguration configuration) : base(_cache, configuration)
        {

        }

        [HttpGet]
        public List<PlayerCurrent> GetPlayers(int worldId)
        {
            string cacheKey = $"Players[{worldId}]"; // Budowa klucza
            if (!_memoryCache.TryGetValue(cacheKey, out List<PlayerCurrent> cacheValue)) //Próba otrzymania wartości
            {
                cacheValue = new PlayerService().GetPlayers(worldId); // Wykonanie zapytania do bazy danych
                var options = new MemoryCacheEntryOptions() //Inicjalizacja obiektu właściwości pamięci podręcznej
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30)); // Ustawienie czasu wygaśnięcia klucza na okres 30 minut

                _memoryCache.Set(cacheKey, cacheValue, options); // Ustawienie wartości klucza 
            }
            return cacheValue; // Zwrócenie zawartości użytkownikowi
        }
    }
}
