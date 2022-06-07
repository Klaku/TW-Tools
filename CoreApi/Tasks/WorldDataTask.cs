using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApi.Models.DB;
using CoreApi.Models.App;
using CoreApi.Helpers;
using NLog;
using CoreApi.Requests;
using System.Threading;
using Hangfire;

namespace CoreApi.Tasks
{
    public class WorldDataTask
    {
        readonly World _world;
        readonly Logger _logger;
        public WorldDataTask(World world)
        {
            _world = world;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Process()
        {
            
            
            using (MonitoredScope proces_scope = new MonitoredScope($"Miner.World[{_world.SubDomain}]", _logger))
            {
                try
                {

                    List<PlayerModel> players = new List<PlayerModel>();
                    List<TribeModel> tribes = new List<TribeModel>();
                    List<VillageModel> villages = new List<VillageModel>();

                    List<StatModel> player_ra = new List<StatModel>();
                    List<StatModel> player_ro = new List<StatModel>();
                    List<StatModel> player_all = new List<StatModel>();

                    List<StatModel> tribe_ra = new List<StatModel>();
                    List<StatModel> tribe_ro = new List<StatModel>();
                    List<StatModel> tribe_all = new List<StatModel>();

                    using (MonitoredScope scope = new MonitoredScope($"Miner.World[{_world.SubDomain}].Fetch", _logger))
                    {
                        var task_players = new PlayerRequest().FetchList(_world);
                        task_players.Wait();
                        players = task_players.Result;
                        scope.Debug($"Fetched {players.Count} players");

                        var task_tribes = new TribeRequest().FetchList(_world);
                        task_tribes.Wait();
                        tribes = task_tribes.Result;
                        scope.Debug($"Fetched {tribes.Count} tribes");

                        var task_villages = new VillageRequest().FetchList(_world);
                        task_villages.Wait();
                        villages = task_villages.Result;
                        scope.Debug($"Fetched {villages.Count} villages");


                        var task_player_ra = new StatRequest().FetchList(_world, "kill_att");
                        task_player_ra.Wait();
                        player_ra = task_player_ra.Result;
                        scope.Debug($"Fetched {player_ra.Count} players att stats");

                        var task_player_ro = new StatRequest().FetchList(_world, "kill_def");
                        task_player_ro.Wait();
                        player_ro = task_player_ro.Result;
                        scope.Debug($"Fetched {player_ro.Count} players def stats");

                        var task_player_all = new StatRequest().FetchList(_world, "kill_all");
                        task_player_all.Wait();
                        player_all = task_player_all.Result;
                        scope.Debug($"Fetched {player_all.Count} players all stats");

                        var task_tribe_ra = new StatRequest().FetchList(_world, "kill_att_tribe");
                        task_tribe_ra.Wait();
                        tribe_ra = task_tribe_ra.Result;
                        scope.Debug($"Fetched {tribe_ra.Count} tribe all stats");

                        var task_tribe_ro = new StatRequest().FetchList(_world, "kill_def_tribe");
                        task_tribe_ro.Wait();
                        tribe_ro = task_tribe_ro.Result;
                        scope.Debug($"Fetched {tribe_ro.Count} tribe def stats");

                        var task_tribe_all = new StatRequest().FetchList(_world, "kill_all_tribe");
                        task_tribe_all.Wait();
                        tribe_all = task_tribe_all.Result;
                        scope.Debug($"Fetched {tribe_all.Count} tribe all stats");
                    }

                    using (MonitoredScope scope = new MonitoredScope($"Miner.World[{_world.SubDomain}].Clear", _logger))
                    {
                        try
                        {
                            var _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
                            while (true)
                            {
                                try
                                {
                                    var History = _db.VillageHistory.Where(x => x.Created < DateTime.Today.AddDays(-8)).Take(1000);
                                    if(History.Count() < 1)
                                    {
                                        break;
                                    }
                                    _db.VillageHistory.RemoveRange(History);
                                    _db.SaveChanges();
                                    scope.Debug("Removed <1000 items");
                                }
                                catch(Exception e)
                                {
                                    scope.Error(e.ToString());
                                    break;
                                }
                            }
                            scope.Debug("Removed old Villages");
                            while (true)
                            {
                                try
                                {
                                    var History = _db.TribeHistory.Where(x => x.Created < DateTime.Today.AddDays(-8)).Take(1000);
                                    if (History.Count() < 1)
                                    {
                                        break;
                                    }
                                    _db.TribeHistory.RemoveRange(History);
                                    _db.SaveChanges();
                                    scope.Debug("Removed <1000 items");
                                }
                                catch (Exception e)
                                {
                                    scope.Error(e.ToString());
                                    break;
                                }
                            }
                            scope.Debug("Removed old Tribes");
                            while (true)
                            {
                                try
                                {
                                    var History = _db.PlayerHistory.Where(x => x.Created < DateTime.Today.AddDays(-8)).Take(1000);
                                    if (History.Count() < 1)
                                    {
                                        break;
                                    }
                                    _db.PlayerHistory.RemoveRange(History);
                                    _db.SaveChanges();
                                    scope.Debug("Removed <1000 items");
                                }
                                catch (Exception e)
                                {
                                    scope.Error(e.ToString());
                                    break;
                                }
                            }
                            scope.Debug("Removed old Players");
                        }
                        catch(Exception e)
                        {
                            scope.Error(e.ToString());
                        }
                    }

                    using (MonitoredScope scope = new MonitoredScope($"Miner.World[{_world.SubDomain}].Store", _logger))
                    {

                        using (MonitoredScope scope_se = new MonitoredScope($"Miner.World[{_world.SubDomain}].Store.Entities", _logger))
                        {
                            List<Thread> threads = new List<Thread>();
                            int playerPagesCount = (int)Math.Ceiling((double)players.Count / 2000);
                            for (var i = 0; i < playerPagesCount; i++)
                            {
                                var pagedPlayers = players.GetRange(i * 2000, Math.Min(2000, players.Count - (i * 2000)));
                                PlayerEntitySaveTask task = new PlayerEntitySaveTask(pagedPlayers, _world, scope_se);
                                Thread t = new Thread(() => task.Start());
                                t.Start();
                                threads.Add(t);
                                if (threads.Count > 20)
                                {
                                    threads.ForEach(thread => thread.Join());
                                    threads.Clear();
                                }
                            }

                            int tribePagesCount = (int)Math.Ceiling((double)tribes.Count / 2000);
                            for (var i = 0; i < tribePagesCount; i++)
                            {
                                var pagedTribes = tribes.GetRange(i * 2000, Math.Min(2000, tribes.Count - (i * 2000)));
                                TribeEntitySaveTask task = new TribeEntitySaveTask(pagedTribes, _world, scope_se);
                                Thread t = new Thread(() => task.Start());
                                t.Start();
                                threads.Add(t);
                                if (threads.Count > 20)
                                {
                                    threads.ForEach(thread => thread.Join());
                                    threads.Clear();
                                }
                            }

                            int villagePagesCount = (int)Math.Ceiling((double)villages.Count / 2000);
                            for (var i = 0; i < villagePagesCount; i++)
                            {
                                var pagedVillages = villages.GetRange(i * 2000, Math.Min(2000, villages.Count - (i * 2000)));
                                VillageEntitySaveTask task = new VillageEntitySaveTask(pagedVillages, _world, scope_se);
                                Thread t = new Thread(() => task.Start());
                                t.Start();
                                threads.Add(t);
                                if (threads.Count > 20)
                                {
                                    threads.ForEach(thread => thread.Join());
                                    threads.Clear();
                                }
                            }

                            threads.ForEach(thread => thread.Join());
                            scope_se.Debug("Threads Joined");
                        }

                        using (MonitoredScope scope_de = new MonitoredScope($"Miner.World[{_world.SubDomain}].Deactivate.Entities", _logger))
                        {
                            int count = RemoveEntitiesTask.Players(players, _world);
                            scope_de.Debug($"{count} Players closed");
                            count = RemoveEntitiesTask.Tribes(tribes, _world);
                            scope_de.Debug($"{count} Tribes closed");
                            count = RemoveEntitiesTask.Villages(villages, _world);
                            scope_de.Debug($"{count} Villages closed");
                        }

                        using (MonitoredScope scope_sh = new MonitoredScope($"Miner.World[{_world.SubDomain}].Store.History", _logger))
                        {
                            List<Thread> threads = new List<Thread>();
                            using (MonitoredScope scope_sh_p = new MonitoredScope($"Miner.World[{_world.SubDomain}].Store.History.Players", _logger))
                            {
                                List<PlayerHistory> history = new List<PlayerHistory>();
                                for (int i = 0; i < players.Count; i++)
                                {
                                    PlayerModel player = players[i];
                                    var ra = player_ra.FirstOrDefault(x => x.Id == player.Id);
                                    var ro = player_ro.FirstOrDefault(x => x.Id == player.Id);
                                    var all = player_all.FirstOrDefault(x => x.Id == player.Id);

                                    history.Add(new PlayerHistory()
                                    {
                                        PlayerId = player.Id,
                                        TribeId = player.TribeId == 0 ? null : player.TribeId,
                                        Points = player.Points,
                                        VillagesCount = player.Villages,
                                        Ranking = player.Rank,
                                        RA = ra != null ? ra.Score : 0,
                                        RO = ro != null ? ro.Score : 0,
                                        RS = all != null ? all.Score : 0,
                                        Created = DateTime.Now,
                                        WorldId = _world.Id,
                                    });
                                }

                                int playerHistoryPagesCount = (int)Math.Ceiling((double)history.Count / 2000);
                                for (var i = 0; i < playerHistoryPagesCount; i++)
                                {
                                    var pagedHistory = history.GetRange(i * 2000, Math.Min(2000, history.Count - (i * 2000)));
                                    PlayerHistorySaveTask task = new PlayerHistorySaveTask(pagedHistory, scope_sh_p);
                                    Thread t = new Thread(() => task.Start());
                                    t.Start();
                                    threads.Add(t);

                                    if (threads.Count > 10)
                                    {
                                        threads.ForEach(thread => thread.Join());
                                        threads.Clear();
                                    }
                                }

                            }

                            using (MonitoredScope tribe_history_scope = new MonitoredScope($"Miner.World[{_world.SubDomain}].Store.History.Tribes", _logger))
                            {
                                List<TribeHistory> history = new List<TribeHistory>();
                                for (int i = 0; i < tribes.Count; i++)
                                {
                                    TribeModel tribe = tribes[i];
                                    var ra = tribe_ra.FirstOrDefault(x => x.Id == tribe.Id);
                                    var ro = tribe_ro.FirstOrDefault(x => x.Id == tribe.Id);
                                    var all = tribe_all.FirstOrDefault(x => x.Id == tribe.Id);

                                    history.Add(new TribeHistory()
                                    {
                                        TribeId = tribe.Id,
                                        WorldId = _world.Id,
                                        Name = tribe.Name,
                                        Tag = tribe.Tag,
                                        Ranking = tribe.Ranking,
                                        Points = tribe.Points,
                                        Villages = tribe.Villages,
                                        RA = ra != null ? ra.Score : 0,
                                        RO = ro != null ? ro.Score : 0,
                                        RS = all != null ? all.Score : 0,
                                        Created = DateTime.Now
                                    });
                                }

                                int tribeHistoryPagesCount = (int)Math.Ceiling((double)history.Count / 2000);
                                for (var i = 0; i < tribeHistoryPagesCount; i++)
                                {
                                    var pagedHistory = history.GetRange(i * 2000, Math.Min(2000, history.Count - (i * 2000)));
                                    TribeHistorySaveTask task = new TribeHistorySaveTask(pagedHistory, tribe_history_scope);
                                    Thread t = new Thread(() => task.Start());
                                    t.Start();
                                    threads.Add(t);

                                    if (threads.Count > 10)
                                    {
                                        threads.ForEach(thread => thread.Join());
                                        threads.Clear();
                                    }
                                }
                            }

                            using (MonitoredScope village_history_scope = new MonitoredScope($"Miner.World[{_world.SubDomain}].Store.History.Villages", _logger))
                            {
                                List<VillageHistory> history = new List<VillageHistory>();
                                for (int i = 0; i < villages.Count; i++)
                                {
                                    VillageModel village = villages[i];

                                    history.Add(new VillageHistory()
                                    {
                                        Points = village.Points,
                                        VillageId = village.Id,
                                        WorldId = _world.Id,
                                        PlayerId = village.PlayerId == 0 ? null : village.PlayerId,
                                        Created = DateTime.Now
                                    });
                                }

                                int villageHistoryPagesCount = (int)Math.Ceiling((double)history.Count / 2000);
                                for (var i = 0; i < villageHistoryPagesCount; i++)
                                {
                                    var pagedHistory = history.GetRange(i * 2000, Math.Min(2000, history.Count - (i * 2000)));
                                    VillageHistorySaveTask task = new VillageHistorySaveTask(pagedHistory, village_history_scope);
                                    Thread t = new Thread(() => task.Start());
                                    t.Start();
                                    threads.Add(t);

                                    if (threads.Count > 10)
                                    {
                                        threads.ForEach(thread => thread.Join());
                                        threads.Clear();
                                    }
                                }
                            }

                            threads.ForEach(thread => thread.Join());
                            scope_sh.Debug("Threads Joined");
                        }
                    }

                }
                catch (Exception ex)
                {
                    proces_scope.Error(ex.ToString());
                }
            }
        }
    }
}
