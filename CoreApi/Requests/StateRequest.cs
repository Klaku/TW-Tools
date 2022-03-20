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
    public class StatRequest
    {
        Logger _logger;

        public StatRequest()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<List<StatModel>> FetchList(World world, string statType)
        {
            List<StatModel> stats = new List<StatModel>();
            using (MonitoredScope scope = new MonitoredScope($"Miner.World[{world.SubDomain}].Fetch.Stat.{statType}", _logger))
            {
                List<string> rows = await Request.Fetch(world, $"/map/{statType}.txt", scope);
                rows.Where(x => x != "").ToList().ForEach(row =>
                {
                    try
                    {
                        stats.Add(new StatModel(row));
                    }
                    catch (Exception e)
                    {
                        scope.Error(e.Message);
                    }
                });
                return stats;
            }
        }
    }
}
