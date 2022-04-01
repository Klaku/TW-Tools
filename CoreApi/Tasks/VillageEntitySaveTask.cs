using CoreApi.Helpers;
using CoreApi.Models.App;
using CoreApi.Models.DB;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApi.Tasks
{
    public class VillageEntitySaveTask
    {
        private readonly List<VillageModel> _villages;
        private readonly CustomContext _db;
        private readonly MonitoredScope _scope;
        private readonly World _world;
        public VillageEntitySaveTask(List<VillageModel> villages, World world, MonitoredScope scope)
        {
            _villages = villages;
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
            _scope = scope;
            _world = world;
        }

        public void Start()
        {
            try
            {
                _scope.Debug($"Running Task for {_villages.Count} items in thread {Thread.CurrentThread.ManagedThreadId}");
                int counter = 0;
                _villages.ForEach(village =>
                {
                    try
                    {
                        Village dbVillage = _db.Village.FirstOrDefault(x => x.Id == village.Id && x.WorldId == _world.Id);
                        if (dbVillage == null)
                        {
                            _db.Village.Add(new Village()
                            {
                                Id = village.Id,
                                PositionX = village.X,
                                PositionY = village.Y,
                                Active = true,
                                WorldId = _world.Id
                            });
                            counter++;
                        }
                    }catch(Exception ex)
                    {
                        _scope.Error(ex.Message);
                    }
                });
                _db.SaveChanges();
                _scope.Debug($"Saved {counter} Villages Entities");
            }
            catch (Exception ex)
            {
                _scope.Error(ex.Message);
            }
        }
    }
    public class VillageHistorySaveTask
    {
        private List<VillageHistory> _villages;
        private CustomContext _db;
        private MonitoredScope _scope;
        public VillageHistorySaveTask(List<VillageHistory> villages, MonitoredScope scope)
        {
            _villages = villages;
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
            _scope = scope;
        }

        public void Start()
        {
            try
            {
                _scope.Debug($"Running Task for {_villages.Count} items");
                for (var i = 0; i < _villages.Count; i++)
                {
                    _db.VillageHistory.Add(_villages[i]);
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _scope.Error(ex.Message);
            }
        }
    }

}
