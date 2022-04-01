using System;
using System.Collections.Generic;
using CoreApi.Helpers;
using NLog;
using CoreApi.Models.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CoreApi.Tasks
{
    public class RebuildVillages
    {
        private readonly List<Village> _villages;
        private readonly Logger _logger;
        private readonly CustomContext _db;
        private readonly World _world;
        private readonly int _index;
        public RebuildVillages(List<Village> villages, Logger logger, World world, int index)
        {
            _villages = villages;
            _logger = logger;
            _world = world;
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
            _index = index;
        }

        public void Start()
        {
            using (MonitoredScope scope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}].Villages[{_index}]", _logger))
            {
                List<VillageCurrent> currentVillages = new List<VillageCurrent>();
                _villages.ForEach(village =>
                {
                    try
                    {
                        VillageHistory villageHistory = _db.VillageHistory
                        .OrderByDescending(x => x.Created)
                        .Where(x => x.VillageId == village.Id && x.WorldId == _world.Id)
                        .FirstOrDefault();
                        VillageHistory villageHistory24 = _db.VillageHistory
                        .OrderByDescending(x => x.Created)
                        .Where(x => x.VillageId == village.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-1))
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
                                Points24 = villageHistory24 == null ? -1 : villageHistory24.Points,
                                Points30 = villageHistory30 == null ? -1 : villageHistory30.Points,
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
                    }
                    catch (Exception exc)
                    {
                        scope.Error(exc.Message);
                    }

                });
                scope.Debug($"Mapped {_villages.Count} Villages");

                for (int i = 0; i < currentVillages.Count; i++)
                {
                    VillageCurrent village = _db.VillageCurrent.FirstOrDefault(x => x.VillageId == currentVillages[i].VillageId && x.WorldId == currentVillages[i].WorldId);
                    if (village != null)
                    {
                        village.Points = currentVillages[i].Points;
                        village.Points7 = currentVillages[i].Points7;
                        village.Points24 = currentVillages[i].Points24;
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
                scope.Debug($"Updated {_villages.Count} Villages");
            }
        }
    }
}
