using System.Collections.Generic;
using WebApi.Models;
using CoreApi.Models.DB;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Services
{
    public class PlayerService
    {
        readonly CustomContext _db;

        public PlayerService()
        {
            _db = new CustomContextFactory().CreateDbContext(null);
        }

        public List<PlayerCurrent> GetPlayers(int worldId)
        {
            List<PlayerCurrent> items =
                _db.PlayerCurrents
                .Include(x => x.Player)
                .Where(x => x.WorldId == worldId && x.Player.Active == true)
                .OrderBy(x => x.Ranking)
                .ToList();
            return items.Select(player =>
            {
                player.Player = null;
                return player;
            }).ToList();
        }
    }
}
