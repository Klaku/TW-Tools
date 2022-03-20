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
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id)
                            .FirstOrDefault();
                            PlayerHistory playerHistory7 = _db.PlayerHistory
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-7))
                            .FirstOrDefault();
                            PlayerHistory playerHistory30 = _db.PlayerHistory
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-30))
                            .FirstOrDefault();
                            if (playerHistory != null)
                            {
                                currentPlayers.Add(new PlayerCurrent()
                                {
                                    PlayerId = player.Id,
                                    TribeId = playerHistory.TribeId,
                                    WorldId = _world.Id,
                                    Name = player.Name,
                                    Points = playerHistory.Points,
                                    Points7 = playerHistory7 == null ? -1 : playerHistory7.Points,
                                    Points30 = playerHistory30 == null ? -1 : playerHistory30.Points,
                                    VillagesCount = playerHistory.VillagesCount,
                                    VillagesCount7 = playerHistory7 == null ? -1 : playerHistory7.VillagesCount,
                                    VillagesCount30 = playerHistory30 == null ? -1 : playerHistory30.VillagesCount,
                                    Ranking = playerHistory.Ranking,
                                    Ranking7 = playerHistory7 == null ? -1 : playerHistory7.Ranking,
                                    Ranking30 = playerHistory30 == null ? -1 : playerHistory30.Ranking,
                                    RA = playerHistory.RA,
                                    RA7 = playerHistory7 == null ? -1 : playerHistory7.RA,
                                    RA30 = playerHistory30 == null ? -1 : playerHistory30.RA,
                                    RO = playerHistory.RO,
                                    RO7 = playerHistory7 == null ? -1 : playerHistory7.RO,
                                    RO30 = playerHistory30 == null ? -1 : playerHistory30.RO,
                                    RS = playerHistory.RS,
                                    RS7 = playerHistory7 == null ? -1 : playerHistory7.RS,
                                    RS30 = playerHistory30 == null ? -1 : playerHistory30.RS,
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
                                player.Points7 = currentPlayers[i].Points7;
                                player.Points30 = currentPlayers[i].Points30;
                                player.VillagesCount = currentPlayers[i].VillagesCount;
                                player.VillagesCount7 = currentPlayers[i].VillagesCount7;
                                player.VillagesCount30 = currentPlayers[i].VillagesCount30;
                                player.Ranking = currentPlayers[i].Ranking;
                                player.Ranking7 = currentPlayers[i].Ranking7;
                                player.Ranking30 = currentPlayers[i].Ranking30;
                                player.RA = currentPlayers[i].RA;
                                player.RA7 = currentPlayers[i].RA7;
                                player.RA30 = currentPlayers[i].RA30;
                                player.RO = currentPlayers[i].RO;
                                player.RO7 = currentPlayers[i].RO7;
                                player.RO30 = currentPlayers[i].RO30;
                                player.RS = currentPlayers[i].RS;
                                player.RS7 = currentPlayers[i].RS7;
                                player.RS30 = currentPlayers[i].RS30;
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
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.TribeId == tribe.Id && x.WorldId == _world.Id)
                            .FirstOrDefault();
                            TribeHistory tribeHistory7 = _db.TribeHistory
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.TribeId == tribe.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-7))
                            .FirstOrDefault();
                            TribeHistory tribeHistory30 = _db.TribeHistory
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.TribeId == tribe.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-30))
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
                                    RA7 = tribeHistory7 == null ? -1 : tribeHistory7.RA,
                                    RA30 = tribeHistory30 == null ? -1 : tribeHistory30.RA,
                                    RO = tribeHistory.RO,
                                    RO7 = tribeHistory7 == null ? -1 : tribeHistory7.RO,
                                    RO30 = tribeHistory30 == null ? -1 : tribeHistory30.RO,
                                    RS = tribeHistory.RS,
                                    RS7 = tribeHistory7 == null ? -1 : tribeHistory7.RS,
                                    RS30 = tribeHistory30 == null ? -1 : tribeHistory30.RS,
                                    Ranking = tribeHistory.Ranking,
                                    Ranking7 = tribeHistory7 == null ? -1 : tribeHistory7.Ranking,
                                    Ranking30 = tribeHistory30 == null ? -1 : tribeHistory30.Ranking,
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
                                tribe.RA7 = currentTribes[i].RA7;
                                tribe.RA30 = currentTribes[i].RA30;
                                tribe.RO = currentTribes[i].RO;
                                tribe.RO7 = currentTribes[i].RO7;
                                tribe.RO30 = currentTribes[i].RO30;
                                tribe.RS = currentTribes[i].RS;
                                tribe.RS7 = currentTribes[i].RS7;
                                tribe.RS30 = currentTribes[i].RS30;
                                tribe.Ranking = currentTribes[i].Ranking;
                                tribe.Ranking7 = currentTribes[i].Ranking7;
                                tribe.Ranking30 = currentTribes[i].Ranking30;
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
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.VillageId == village.Id && x.WorldId == _world.Id)
                            .FirstOrDefault();
                            VillageHistory villageHistory7 = _db.VillageHistory
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.VillageId == village.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-7))
                            .FirstOrDefault();
                            VillageHistory villageHistory30 = _db.VillageHistory
                            .OrderByDescending(x => x.Created)
                            .Where(x => x.VillageId == village.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-30))
                            .FirstOrDefault();
                            var CurrentOwner = _db.PlayerCurrents.FirstOrDefault(x => x.PlayerId == villageHistory.PlayerId);
                            if (villageHistory != null)
                            {
                                currentVillages.Add(new VillageCurrent()
                                {
                                    PositionX = village.PositionX,
                                    PositionY = village.PositionY,
                                    Points = villageHistory.Points,
                                    Points7 = villageHistory7 == null ? -1 : villageHistory7.Points,
                                    Points30 = villageHistory7 == null ? -1 : villageHistory30.Points,
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
                                village.Points7 = currentVillages[i].Points7;
                                village.Points30 = currentVillages[i].Points30;
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
