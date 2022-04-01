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
    public class VillageController : ApiController
    {
        public VillageController(IMemoryCache _cache, IConfiguration configuration) : base(_cache, configuration)
        {
        }
        [HttpGet]
        public List<VillageCurrent> GetVillagesByPlayerId(int worldId, int playerId)
        {
            string cacheKey = $"VbpId[{worldId}][{playerId}]";
            if (!_memoryCache.TryGetValue(cacheKey, out List<VillageCurrent> cacheValue))
            {
                cacheValue = new VillageService().GetVillagesByPlayerId(worldId, playerId);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, cacheValue, options);
            }
            return cacheValue;
        }

        [HttpGet]
        public List<VillageCurrent> GetVillagesByTribeId(int worldId, int tribeId)
        {
            string cacheKey = $"VbtId[{worldId}][{tribeId}]";
            if (!_memoryCache.TryGetValue(cacheKey, out List<VillageCurrent> cacheValue))
            {
                cacheValue = new VillageService().GetVillagesByTribeId(worldId, tribeId);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, cacheValue, options);
            }
            return cacheValue;
        }
    }
}
