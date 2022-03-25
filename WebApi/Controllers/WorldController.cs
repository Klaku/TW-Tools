using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Services;
using System.Threading;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        public List<dtoWorldModel> GetWorldList()
        {
            return new WorldService().GetList();
        }
    }
}
