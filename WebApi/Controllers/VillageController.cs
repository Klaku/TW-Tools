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
    public class VillageController : ControllerBase
    {
        [HttpGet]
        public List<VillageCurrent> GetVillagesByPlayerId(int worldId, int playerId)
        {
            return new VillageService().GetVillagesByPlayerId(worldId, playerId);
        }

        [HttpGet]
        public List<VillageCurrent> GetVillagesByTribeId(int worldId, int tribeId)
        {
            return new VillageService().GetVillagesByTribeId(worldId, tribeId);
        }
    }
}
