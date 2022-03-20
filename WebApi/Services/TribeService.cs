using System.Collections.Generic;
using WebApi.Models;
using CoreApi.Models.DB;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public class TribeService
    {
        readonly CustomContext _db;
        public TribeService()
        {
            _db = new CustomContextFactory().CreateDbContext(null);
        }
        public List<dtoTribeModelMinimal> ListTribesByWorldId(int worldId)
        {
            return _db.TribeCurrent
                .Include(x => x.Tribe)
                .Where(x => x.WorldId == worldId && x.Tribe.Active == true)
                .Select(tribe => new dtoTribeModelMinimal()
                {
                    Id = tribe.Id,
                    Name = tribe.Name,
                    Tag = tribe.Tag,
                })
                .ToList();
        }

        public List<TribeCurrent> ListTribeData(int worldId, int skip, int top)
        {
            return _db.TribeCurrent
                .Include(x => x.Tribe)
                .Where(x => x.WorldId == worldId && x.Tribe.Active == true)
                .OrderBy(x => x.Ranking)
                .Skip(skip)
                .Take(top)
                .ToList();
        }
    }
}
