using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System.Configuration;
using System;
namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WorldController : ApiController
    {
        public WorldController(IMemoryCache _cache, IConfiguration configuration) : base(_cache, configuration)
        {

        }
        public List<dtoWorldModel> GetWorldList()
        {
            return new WorldService().GetList();
        }
    }
}
