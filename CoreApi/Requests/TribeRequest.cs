using CoreApi.Helpers;
using CoreApi.Models.App;
using CoreApi.Models.DB;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApi.Requests
{
    public class TribeRequest
    {
        readonly Logger _logger;

        public TribeRequest()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<List<TribeModel>> FetchList(World world)
        {
            List<TribeModel> tribes = new List<TribeModel>();
            using (MonitoredScope scope = new MonitoredScope($"Miner.World[{world.SubDomain}].Fetch.Tribes", _logger))
            {
                List<string> rows = await Request.Fetch(world, "/map/ally.txt", scope);
                rows.ForEach(row =>
                {
                    try
                    {
                        tribes.Add(new TribeModel(row));
                    }
                    catch (Exception e)
                    {
                        scope.Error(e.Message);
                    }
                });
                return tribes;
            }
        }
    }
}
