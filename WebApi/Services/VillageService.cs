using System.Collections.Generic;
using WebApi.Models;
using CoreApi.Models.DB;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public class VillageService
    {
        CustomContext _db;
        public VillageService()
        {
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
        }

        public List<VillageCurrent> GetVillagesByPlayerId(int worldId, int playerId)
        {
            List<VillageCurrent> data = _db.VillageCurrent
                .Include(x => x.Village)
                .Where(x => x.WorldId == worldId && x.Village.Active == true && x.PlayerId == playerId)
                .OrderBy(x => x.Points)
                .ToList();
            List<VillageCurrent> result = new List<VillageCurrent>();
            data.ForEach(x =>
            {
                x.Village = null;
                result.Add(x);
            });
            return result;
        }

        public List<VillageCurrent> GetVillagesByTribeId(int worldId, int tribeId)
        {
            List<VillageCurrent> data = _db.VillageCurrent
                .Include(x => x.Village)
                .Where(x => x.WorldId == worldId && x.Village.Active == true && x.TribeId == tribeId)
                .OrderBy(x => x.Points)
                .ToList();
            List<VillageCurrent> result = new List<VillageCurrent>();
            data.ForEach(x =>
            {
                x.Village = null;
                result.Add(x);
            });
            return result;
        }


    }
}
