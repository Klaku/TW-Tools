using CoreApi.Helpers;
using CoreApi.Models.App;
using CoreApi.Models.DB;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApi.Requests
{
    public class VillageRequest
    {
        readonly Logger _logger;

        public VillageRequest()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<List<VillageModel>> FetchList(World world)
        {
            List<VillageModel> villages = new List<VillageModel>();
            using (MonitoredScope scope = new MonitoredScope($"Miner.World[{world.SubDomain}].Fetch.Villages", _logger))
            {
                List<string> rows = await Request.Fetch(world, "/map/village.txt", scope);
                rows.ForEach(row =>
                {
                    try
                    {
                        villages.Add(new VillageModel(row));
                    }
                    catch (Exception e)
                    {
                        scope.Error(e.Message);
                    }
                });
                return villages;
            }
        }
    }
}
