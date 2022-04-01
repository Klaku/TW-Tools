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
    public class RebuildTribes
    {
        private readonly List<Tribe> _tribes;
        private readonly Logger _logger;
        private readonly CustomContext _db;
        private readonly World _world;
        private readonly int _index;
        public RebuildTribes(List<Tribe> tribes, Logger logger, World world, int index)
        {
            _tribes = tribes;
            _logger = logger;
            _world = world;
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
            _index = index;
        }

        public void Start()
        {
            using (MonitoredScope scope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}].Tribes[{_index}]", _logger))
            {
                List<TribeCurrent> currentTribes = new List<TribeCurrent>();
                
                _tribes.ForEach(tribe =>
                {
                    TribeHistory tribeHistory = _db.TribeHistory
                    .OrderByDescending(x => x.Created)
                    .Where(x => x.TribeId == tribe.Id && x.WorldId == _world.Id)
                    .FirstOrDefault();
                    TribeHistory tribeHistory24 = _db.TribeHistory
                    .OrderByDescending(x => x.Created)
                    .Where(x => x.TribeId == tribe.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-1))
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
                            Points = tribeHistory.Points,
                            Points7 = tribeHistory7 == null ? -1 : tribeHistory7.Points,
                            Points24 = tribeHistory24 == null ? -1 : tribeHistory24.Points,
                            Points30 = tribeHistory30 == null ? -1 : tribeHistory30.Points,
                            Villages = tribeHistory.Villages,
                            Villages7 = tribeHistory7 == null ? -1 : tribeHistory7.Villages,
                            Villages24 = tribeHistory24 == null ? -1 : tribeHistory24.Villages,
                            Villages30 = tribeHistory30 == null ? -1 : tribeHistory30.Villages,
                            RA = tribeHistory.RA,
                            RA7 = tribeHistory7 == null ? -1 : tribeHistory7.RA,
                            RA24 = tribeHistory24 == null ? -1 : tribeHistory24.RA,
                            RA30 = tribeHistory30 == null ? -1 : tribeHistory30.RA,
                            RO = tribeHistory.RO,
                            RO7 = tribeHistory7 == null ? -1 : tribeHistory7.RO,
                            RO24 = tribeHistory24 == null ? -1 : tribeHistory24.RO,
                            RO30 = tribeHistory30 == null ? -1 : tribeHistory30.RO,
                            RS = tribeHistory.RS,
                            RS7 = tribeHistory7 == null ? -1 : tribeHistory7.RS,
                            RS24 = tribeHistory24 == null ? -1 : tribeHistory24.RS,
                            RS30 = tribeHistory30 == null ? -1 : tribeHistory30.RS,
                            Ranking = tribeHistory.Ranking,
                            Ranking7 = tribeHistory7 == null ? -1 : tribeHistory7.Ranking,
                            Ranking24 = tribeHistory24 == null ? -1 : tribeHistory24.Ranking,
                            Ranking30 = tribeHistory30 == null ? -1 : tribeHistory30.Ranking,
                        });
                    }
                    else
                    {
                        scope.Error($"History for tribe {tribe.Id} not found");
                    }

                });
                scope.Debug($"Mapped {_tribes.Count} Tribes");

                for (int i = 0; i < currentTribes.Count; i++)
                {
                    TribeCurrent tribe = _db.TribeCurrent.FirstOrDefault(x => x.TribeId == currentTribes[i].TribeId && x.WorldId == currentTribes[i].WorldId);
                    if (tribe != null)
                    {
                        tribe.Name = currentTribes[i].Name;
                        tribe.Tag = currentTribes[i].Tag;
                        tribe.Points = currentTribes[i].Points;
                        tribe.Points7 = currentTribes[i].Points7;
                        tribe.Points24 = currentTribes[i].Points24;
                        tribe.Points30 = currentTribes[i].Points30;
                        tribe.Villages = currentTribes[i].Villages;
                        tribe.Villages7 = currentTribes[i].Villages7;
                        tribe.Villages24 = currentTribes[i].Villages24;
                        tribe.Villages30 = currentTribes[i].Villages30;
                        tribe.RA = currentTribes[i].RA;
                        tribe.RA7 = currentTribes[i].RA7;
                        tribe.RA24 = currentTribes[i].RA24;
                        tribe.RA30 = currentTribes[i].RA30;
                        tribe.RO = currentTribes[i].RO;
                        tribe.RO7 = currentTribes[i].RO7;
                        tribe.RO24 = currentTribes[i].RO24;
                        tribe.RO30 = currentTribes[i].RO30;
                        tribe.RS = currentTribes[i].RS;
                        tribe.RS7 = currentTribes[i].RS7;
                        tribe.RS24 = currentTribes[i].RS24;
                        tribe.RS30 = currentTribes[i].RS30;
                        tribe.Ranking = currentTribes[i].Ranking;
                        tribe.Ranking7 = currentTribes[i].Ranking7;
                        tribe.Ranking24 = currentTribes[i].Ranking24;
                        tribe.Ranking30 = currentTribes[i].Ranking30;
                    }
                    else
                    {
                        _db.TribeCurrent.Add(currentTribes[i]);
                    }

                    if (i % 1000 == 0)
                    {
                        _db.SaveChanges();
                    }
                }
                _db.SaveChanges();
                scope.Debug($"Updated {_tribes.Count} Tribes");
            }
        }
    }
}
