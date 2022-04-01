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
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Tasks
{
    public class RebuildDbCache
    {
        private readonly Logger _logger;
        private readonly CustomContext _db;

        public RebuildDbCache()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
        }

        public void Process()
        {
            using(MonitoredScope scope = new MonitoredScope($"Miner.RebuildCache", _logger))
            {
                List<World> worlds = _db.World.Where(x => x.IsActive == true).ToList();
                List<Thread> threads = new List<Thread>();
                foreach (World w in worlds)
                {
                    RebuildWorldCache task = new RebuildWorldCache(w);
                    Thread thread = new Thread(() => task.Start());
                    thread.Start();
                    threads.Add(thread);
                }

                threads.ForEach(thread => thread.Join());
                scope.Debug("Threads Joined");
            }
        }
    }

    public class RebuildWorldCache
    {
        private readonly Logger _logger;
        private readonly CustomContext _db;
        private readonly World _world;
        public RebuildWorldCache(World world)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
            _world = world;
        }
        public void Start()
        {
            using (MonitoredScope scope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}]", _logger))
            {
                using (MonitoredScope subScope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}].Players", _logger))
                {
                    try
                    {
                        List<Player> players = new List<Player>();
                        players = _db.Player.Where(x => x.WorldId == _world.Id && x.Active == true).ToList();
                        subScope.Debug($"Received {players.Count} Players");
                        List<Thread> threads = new List<Thread>();
                        int page_size = 50;
                        int paged = (int)Math.Ceiling((double)players.Count / page_size);
                        for (var i = 0; i < paged; i++)
                        {
                            var range = players.GetRange(i * page_size, Math.Min(page_size, players.Count - (i * page_size)));
                            RebuildPlayers task = new RebuildPlayers(range,_logger, _world, i);
                            Thread t = new Thread(() => task.Start());
                            t.Start();
                            threads.Add(t);
                        }
                        threads.ForEach(thread => thread.Join());
                        subScope.Debug("Threads joined");

                    }
                    catch (Exception ex)
                    {
                        subScope.Error(ex.Message);
                    }
                }

                using(MonitoredScope subScope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}].Tribes", _logger))
                {
                    try
                    {
                        List<Tribe> tribes = new List<Tribe>();
                        tribes = _db.Tribe.Where(x => x.WorldId == _world.Id && x.Active == true).ToList();
                        scope.Debug($"Received {tribes.Count} Tribes");
                        List<Thread> threads = new List<Thread>();
                        int page_size = 50;
                        int paged = (int)Math.Ceiling((double)tribes.Count / page_size);
                        for (var i = 0; i < paged; i++)
                        {
                            var range = tribes.GetRange(i * page_size, Math.Min(page_size, tribes.Count - (i * page_size)));
                            RebuildTribes task = new RebuildTribes(range, _logger, _world, i);
                            Thread t = new Thread(() => task.Start());
                            t.Start();
                            threads.Add(t);
                        }
                        threads.ForEach(thread => thread.Join());
                        subScope.Debug("Threads joined");
                    }
                    catch(Exception ex)
                    {
                        subScope.Error(ex.Message);
                    }
                }

                using(MonitoredScope subScope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}].Villages", _logger))
                {
                    try
                    {
                        List<Village> villages = new List<Village>();
                        villages = _db.Village.Where(x => x.WorldId == _world.Id && x.Active == true).ToList();
                        subScope.Debug($"Received {villages.Count} Villages");
                        List<Thread> threads = new List<Thread>();
                        int page_size = 50;
                        int paged = (int)Math.Ceiling((double)villages.Count / page_size);
                        for (var i = 0; i < paged; i++)
                        {
                            var range = villages.GetRange(i * page_size, Math.Min(page_size, villages.Count - (i * page_size)));
                            RebuildVillages task = new RebuildVillages(range, _logger, _world, i);
                            Thread t = new Thread(() => task.Start());
                            t.Start();
                            threads.Add(t);
                        }
                        threads.ForEach(thread => thread.Join());
                        subScope.Debug("Threads joined");
                    }
                    catch(Exception ex)
                    {
                        subScope.Error(ex.Message);
                    }
                }

            }
        }
    }
}
