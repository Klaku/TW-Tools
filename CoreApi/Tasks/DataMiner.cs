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
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
        }

        public void Process()
        {
            using (MonitoredScope scope = new MonitoredScope("Miner", _logger)) //Rozpoczęcie monitorowania czasu wykonywania funkcji Process
            {
                List<World> worlds = _db.World.Where(x => x.IsActive == true).ToList();
                List<Thread> threads = new List<Thread>();
                foreach (World w in worlds)
                {
                    WorldDataTask task = new WorldDataTask(w);
                    Thread thread = new Thread(() => task.Process());
                    thread.Start();
                    threads.Add(thread);
                    if(threads.Count % 2 == 0)
                    {
                        threads.ForEach(thread => thread.Join());
                        threads.Clear();
                    }
                }

                threads.ForEach(thread => thread.Join());

                scope.Debug("Threads Joined"); // Wpis informujący ile czasu upłynęło od rozpoczęcia działania bloku

                new RebuildDbCache().Process();
            }// opuszczenie bloku using wymusza uruchomienie destruktora MonitoredScope i umieszczenia informacji i czasie wykonania bloku
        }
    }
}
