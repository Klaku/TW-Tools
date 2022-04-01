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
    public class TribeController : ApiController
    {
        public TribeController(IMemoryCache _cache, IConfiguration configuration) : base(_cache, configuration)
        {
        }
        [HttpGet]
        public List<TribeCurrent> GetTribes(int worldId)
        {
            string cacheKey = $"Tribes[{worldId}]";
            if (!_memoryCache.TryGetValue(cacheKey, out List<TribeCurrent> cacheValue))
            {
                cacheValue = new TribeService().GetTribes(worldId);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(cacheKey, cacheValue, options);
            }
            return cacheValue;
        }
    }
}
