using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        public List<dtoWorldModel> GetWorldList()
        {
            return new WorldService().GetList();
        }
    }
}
