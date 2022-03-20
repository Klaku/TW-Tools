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
    public class PlenerController : ControllerBase
    {
        [HttpGet]
        public List<dtoTribeModelMinimal> GetTribes(int id)
        {
            return new List<dtoTribeModelMinimal>();
        }
    }
}
