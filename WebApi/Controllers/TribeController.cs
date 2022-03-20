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
    public class TribeController : ControllerBase
    {
        [HttpGet]
        public List<dtoTribeModelMinimal> GetTribeList(int worldId)
        {
            return new TribeService().ListTribesByWorldId(worldId);
        }

        [HttpGet]
        public List<TribeCurrent> ListTribeData(int worldId, int skip, int top)
        {
            return new TribeService().ListTribeData(worldId, skip, top);
        }
    }
}
