﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApi.Models.DB;
using System.Linq;
using System.Collections.Generic;
using WebApi.Services;
using WebApi.Models;
using System.Threading;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TribeController : ControllerBase
    {
        [HttpGet]
        public List<TribeCurrent> GetTribes(int worldId)
        {
            return new TribeService().GetTribes(worldId);
        }
    }
}
