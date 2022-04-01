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
            string cacheKey = $"Players[{worldId}]";
            if (!_memoryCache.TryGetValue(cacheKey, out List<PlayerCurrent> cacheValue))
            {
                cacheValue = new PlayerService().GetPlayers(worldId);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, cacheValue, options);
            }
            return cacheValue;
        }
    }
}
