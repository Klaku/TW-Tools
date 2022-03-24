using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApi.Models.DB;
using System.Linq;
using System.Collections.Generic;
using WebApi.Services;
using WebApi.Models;
namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpGet]
        public List<PlayerCurrent> GetPlayers(int worldId)
        {
            return new PlayerService().GetPlayers(worldId);
        }
    }
}
