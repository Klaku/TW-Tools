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
    public class TribeEntitySaveTask
    {
        private readonly List<TribeModel> _tribes;
        private readonly CustomContext _db;
        private readonly MonitoredScope _scope;
        private readonly World _world;
        public TribeEntitySaveTask(List<TribeModel> tribes, World world, MonitoredScope scope)
        {
            _tribes = tribes;
            _db = new CustomContextFactory().CreateDbContext(null);
            _scope = scope;
            _world = world;
        }

        public void Start()
        {
            try
            {
                _scope.Debug($"Running Task for {_tribes.Count} items in thread {Thread.CurrentThread.ManagedThreadId}");
                int counter = 0;
                _tribes.ForEach(tribe =>
                {
                    try
                    {
                        Tribe dbTribe = _db.Tribe.FirstOrDefault(x => x.Id == tribe.Id && x.WorldId == _world.Id);
                        if (dbTribe == null)
                        {
                            _db.Tribe.Add(new Tribe()
                            {
                                Id = tribe.Id,
                                WorldId = _world.Id,
                            });
                            counter++;
                        }
                    }
                    catch(Exception ex)
                    {
                        _scope.Error(ex.Message);
                    }
                });
                _db.SaveChanges();
                _scope.Debug($"Saved {counter} Tribe Entities");
            }
            catch (Exception ex)
            {
                _scope.Error(ex.Message);
            }
        }
    }

    public class TribeHistorySaveTask
    {
        private List<TribeHistory> _tribes;
        private CustomContext _db;
        private MonitoredScope _scope;
        public TribeHistorySaveTask(List<TribeHistory> tribes, MonitoredScope scope)
        {
            _tribes = tribes;
            _db = new CustomContextFactory().CreateDbContext(null);
            _scope = scope;
        }

        public void Start()
        {
            try
            {
                _scope.Debug($"Running Task for {_tribes.Count} items");
                for (var i = 0; i < _tribes.Count; i++)
                {
                    _db.TribeHistory.Add(_tribes[i]);
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
