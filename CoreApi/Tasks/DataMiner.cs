using System;
using System.Collections.Generic;
using System.Text;
using CoreApi.Helpers;
using NLog;
using CoreApi.Models.DB;
using CoreApi.Models.App;
using System.Linq;
using System.Net;
using System.Net.Http;
using CoreApi.Requests;
using System.Threading.Tasks;
using System.Threading;
using Hangfire;

namespace CoreApi.Tasks
{
    public  class DataMiner
    {
        readonly Logger _logger;
        readonly CustomContext _db;
        public DataMiner()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _db = new CustomContextFactory().CreateDbContext(null);
        }

        public void Process()
        {
            using (MonitoredScope scope = new MonitoredScope("Miner", _logger))
            {
                List<World> worlds = _db.World.Where(x => x.IsActive == true).ToList();
                List<Thread> threads = new List<Thread>();
                foreach (World w in worlds)
                {
                    WorldDataTask task = new WorldDataTask(w);
                    Thread thread = new Thread(() => task.Process());
                    thread.Start();
                    threads.Add(thread);
                }

                threads.ForEach(thread => thread.Join());

                scope.Debug("Threads Joined");

                new RebuildDbCache().Process();
            }
        }
    }
}
