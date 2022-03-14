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
    public class PlayerEntitySaveTask
    {
        private readonly List<PlayerModel> _players;
        private readonly CustomContext _db;
        private readonly MonitoredScope _scope;
        private readonly World _world;
        public PlayerEntitySaveTask(List<PlayerModel> players, World world, MonitoredScope scope)
        {
            _players = players;
            _db = new CustomContextFactory().CreateDbContext(null);
            _scope = scope;
            _world = world;
        }

        public void Start()
        {
            try
            {
                _scope.Debug($"Running Task for {_players.Count} items in thread {Environment.CurrentManagedThreadId}");
                int counter = 0;
                _players.ForEach(player =>
                {
                    try
                    {
                        Player dbPlayer = _db.Player.FirstOrDefault(x => x.Id == player.Id && x.WorldId == _world.Id);
                        if (dbPlayer == null)
                        {
                            _db.Player.Add(new Player()
                            {
                                Id = player.Id,
                                Name = player.Name,
                                WorldId = _world.Id,
                            });
                            counter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        _scope.Error(ex.Message);
                    }
                });
                _db.SaveChanges();
                _scope.Debug($"Saved {counter} Player Entities");
            }
            catch (Exception ex)
            {
                _scope.Error(ex.Message);
            }
        }
    }

    public class PlayerHistorySaveTask
    {
        private readonly List<PlayerHistory> _players;
        private readonly CustomContext _db;
        private readonly MonitoredScope _scope;
        public PlayerHistorySaveTask(List<PlayerHistory> players, MonitoredScope scope)
        {
            _players = players;
            _db = new CustomContextFactory().CreateDbContext(null);
            _scope = scope;
        }

        public void Start()
        {
            try
            {
                _scope.Debug($"Running Task for {_players.Count} items");
                for (var i = 0; i < _players.Count; i++)
                {
                    _db.PlayerHistory.Add(_players[i]);
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
