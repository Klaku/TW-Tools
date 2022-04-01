using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
namespace WebApi.Controllers
{
    public class ApiController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly IMemoryCache _memoryCache;

        public ApiController(IMemoryCache _cache, IConfiguration configuration)
        {
            _configuration = configuration;
            _memoryCache = _cache;
        }
    }
}
