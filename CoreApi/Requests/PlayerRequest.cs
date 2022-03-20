using CoreApi.Helpers;
using CoreApi.Models.App;
using CoreApi.Models.DB;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace CoreApi.Requests
{
    public class PlayerRequest
    {
        readonly Logger _logger;

        public PlayerRequest(){
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<List<PlayerModel>> FetchList(World world)
        {
            List<PlayerModel> players = new List<PlayerModel>();
            using (MonitoredScope scope = new MonitoredScope($"Miner.World[{world.SubDomain}].Fetch.Players", _logger))
            {
                List<string> rows = await Request.Fetch(world, "/map/player.txt", scope);
                rows.Where(x => x != "").ToList().ForEach(row =>
                {
                    try
                    {
                        players.Add(new PlayerModel(row));
                    }
                    catch (Exception e)
                    {
                        scope.Error(e.Message);
                    }
                });
                return players;
            }
        }
    }
}
