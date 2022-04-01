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
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
        }

        public List<TribeCurrent> GetTribes(int worldId)
        {
            List<TribeCurrent> items =
                _db.TribeCurrent
                .Include(x => x.Tribe)
                .Where(x => x.WorldId == worldId && x.Tribe.Active == true)
                .OrderBy(x => x.Ranking)
                .ToList();
            return items.Select(tribe =>
            {
                tribe.Tribe = null;
                return tribe;
            }).ToList();

        }
    }
}
