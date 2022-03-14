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


namespace CoreApi.Tasks
{
    public class RebuildDbCache
    {
        private readonly Logger _logger;
        private readonly CustomContext _db;

        public RebuildDbCache()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _db = new CustomContextFactory().CreateDbContext(null);
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
            _db = new CustomContextFactory().CreateDbContext(null);
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
                        players = _db.Player.Where(x => x.WorldId == _world.Id).ToList();
                        List<PlayerCurrent> currentPlayers = new List<PlayerCurrent>();
                        subScope.Debug($"Received {players.Count} Players");
                        players.ForEach(player =>
                        {
                            PlayerHistory playerHistory = _db.PlayerHistory
                            .OrderBy(x => x.Created)
                            .Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id)
                            .FirstOrDefault();
                            if(playerHistory != null)
                            {
                                currentPlayers.Add(new PlayerCurrent()
                                {
                                    PlayerId = player.Id,
                                    TribeId = playerHistory.TribeId,
                                    WorldId = _world.Id,
                                    Name = player.Name,
                                    Points = playerHistory.Points,
                                    VillagesCount = playerHistory.VillagesCount,
                                    Ranking = playerHistory.Ranking,
                                    RA = playerHistory.RA,
                                    RO = playerHistory.RO,
                                    RS = playerHistory.RS,
                                });
                            }
                            else
                            {
                                scope.Error($"History for player {player.Id} - {player.Name} not found");
                            }
                            
                        });
                        subScope.Debug($"Mapped {players.Count} Players");

                        for (int i = 0; i < currentPlayers.Count; i++)
                        {
                            PlayerCurrent player = _db.PlayerCurrents.FirstOrDefault(x => x.PlayerId == currentPlayers[i].PlayerId && x.WorldId == currentPlayers[i].WorldId);
                            if (player != null)
                            {
                                player.TribeId = currentPlayers[i].TribeId;
                                player.Points = currentPlayers[i].Points;
                                player.VillagesCount = currentPlayers[i].VillagesCount;
                                player.Ranking = currentPlayers[i].Ranking;
                                player.RA = currentPlayers[i].RA;
                                player.RO = currentPlayers[i].RO;
                                player.RS = currentPlayers[i].RS;
                            }
                            else
                            {
                                _db.PlayerCurrents.Add(currentPlayers[i]);
                            }

                            if (i % 1000 == 0)
                            {
                                _db.SaveChanges();
                            }
                        }
                        _db.SaveChanges();
                        subScope.Debug($"Updated {players.Count} Players");
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
                        tribes = _db.Tribe.Where(x => x.WorldId == _world.Id).ToList();
                        List<TribeCurrent> currentTribes = new List<TribeCurrent>();
                        subScope.Debug($"Received {tribes.Count} Tribes");
                        tribes.ForEach(tribe =>
                        {
                            TribeHistory tribeHistory = _db.TribeHistory
                            .OrderBy(x => x.Created)
                            .Where(x => x.TribeId == tribe.Id && x.WorldId == _world.Id)
                            .FirstOrDefault();
                            if (tribeHistory != null)
                            {
                                currentTribes.Add(new TribeCurrent()
                                {
                                    WorldId = _world.Id,
                                    TribeId = tribe.Id,
                                    Name = tribeHistory.Name,
                                    Tag = tribeHistory.Tag,
                                    RA = tribeHistory.RA,
                                    RO = tribeHistory.RO,
                                    RS = tribeHistory.RS,
                                    Ranking = tribeHistory.Ranking,
                                });
                            }
                            else
                            {
                                scope.Error($"History for tribe {tribe.Id} not found");
                            }

                        });
                        subScope.Debug($"Mapped {tribes.Count} Tribes");

                        for(int i = 0; i < currentTribes.Count; i++)
                        {
                            TribeCurrent tribe = _db.TribeCurrent.FirstOrDefault(x => x.TribeId == currentTribes[i].TribeId && x.WorldId == currentTribes[i].WorldId);
                            if(tribe != null)
                            {
                                tribe.Name = currentTribes[i].Name;
                                tribe.Tag = currentTribes[i].Tag;
                                tribe.RA = currentTribes[i].RA;
                                tribe.RO = currentTribes[i].RO;
                                tribe.RS = currentTribes[i].RS;
                                tribe.Ranking = currentTribes[i].Ranking;
                            }
                            else
                            {
                                _db.TribeCurrent.Add(currentTribes[i]);
                            }

                            if(i%1000 == 0)
                            {
                                _db.SaveChanges();
                            }
                        }
                        _db.SaveChanges();
                        subScope.Debug($"Updated {tribes.Count} Tribes");
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
                        villages = _db.Village.Where(x => x.WorldId == _world.Id).ToList();
                        List<VillageCurrent> currentVillages = new List<VillageCurrent>();
                        subScope.Debug($"Received {villages.Count} Villages");
                        villages.ForEach(village =>
                        {
                            VillageHistory villageHistory = _db.VillageHistory
                            .OrderBy(x => x.Created)
                            .Where(x => x.VillageId == village.Id && x.WorldId == _world.Id)
                            .FirstOrDefault();
                            var CurrentOwner = _db.PlayerCurrents.FirstOrDefault(x => x.PlayerId == villageHistory.PlayerId);
                            if (villageHistory != null)
                            {
                                currentVillages.Add(new VillageCurrent()
                                {
                                    PositionX = village.PositionX,
                                    PositionY = village.PositionY,
                                    Points = villageHistory.Points,
                                    WorldId = _world.Id,
                                    VillageId = village.Id,
                                    PlayerId = villageHistory.PlayerId,
                                    TribeId = CurrentOwner == null ? null : CurrentOwner.TribeId,
                                });
                            }
                            else
                            {
                                scope.Error($"History for village {village.Id} not found");
                            }
                        });
                        subScope.Debug($"Mapped {villages.Count} Villages");

                        for (int i = 0; i < currentVillages.Count; i++)
                        {
                            VillageCurrent village = _db.VillageCurrent.FirstOrDefault(x => x.VillageId == currentVillages[i].VillageId && x.WorldId == currentVillages[i].WorldId);
                            if (village != null)
                            {
                                village.Points = currentVillages[i].Points;
                                village.PlayerId = currentVillages[i].PlayerId;
                            }
                            else
                            {
                                _db.VillageCurrent.Add(currentVillages[i]);
                            }

                            if (i % 1000 == 0)
                            {
                                _db.SaveChanges();
                            }
                        }
                        _db.SaveChanges();
                        subScope.Debug($"Updated {villages.Count} Villages");
                    }catch(Exception ex)
                    {
                        subScope.Error(ex.Message);
                    }
                }

            }
        }
    }
}
