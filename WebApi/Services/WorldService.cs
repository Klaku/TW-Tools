using CoreApi.Models.DB;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services
{
    public class WorldService
    {
        readonly CustomContext _db;
        public WorldService()
        {
            _db = new CustomContextFactory().CreateDbContext(null);
        }
        public List<dtoWorldModel> GetList()
        {
            return _db.World.Select(world => new dtoWorldModel()
            {
                Id = world.Id,
                Name = world.Name,
                Domain = world.Domain,
                SubDomain = world.SubDomain,
            }).ToList();
        }
    }
}
