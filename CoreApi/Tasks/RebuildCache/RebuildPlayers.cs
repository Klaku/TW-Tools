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
    public class RebuildPlayers
    {
        private readonly List<Player> _players;
        private readonly Logger _logger;
        private readonly CustomContext _db;
        private readonly World _world;
        private readonly int _index;
        public RebuildPlayers(List<Player> players, Logger logger, World world, int index)
        {
            _players = players;
            _logger = logger;
            _world = world;
            _db = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase));
            _index = index;
        }

        public void Start()
        {
            using(MonitoredScope scope = new MonitoredScope($"Miner.RebuildCache.World[{_world.SubDomain}].Players[{_index}]", _logger))
            {
                List<PlayerCurrent> currentPlayers = new List<PlayerCurrent>();
                _players.ForEach(player =>
                {
                    PlayerHistory playerHistory = _db.PlayerHistory.OrderByDescending(x => x.Created).Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id).FirstOrDefault();
                    PlayerHistory playerHistory24 = _db.PlayerHistory.OrderByDescending(x => x.Created).Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-1)).FirstOrDefault();
                    PlayerHistory playerHistory7 = _db.PlayerHistory.OrderByDescending(x => x.Created).Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-7)).FirstOrDefault();
                    PlayerHistory playerHistory30 = _db.PlayerHistory.OrderByDescending(x => x.Created).Where(x => x.PlayerId == player.Id && x.WorldId == _world.Id && x.Created < DateTime.Today.AddDays(-30)).FirstOrDefault();
                    if (playerHistory != null)
                    {
                        currentPlayers.Add(new PlayerCurrent()
                        {
                            PlayerId = player.Id,
                            TribeId = playerHistory.TribeId,
                            WorldId = _world.Id,
                            Name = player.Name,
                            Points = playerHistory.Points,
                            Points24 = playerHistory24 == null ? -1 : playerHistory24.Points,
                            Points7 = playerHistory7 == null ? -1 : playerHistory7.Points,
                            Points30 = playerHistory30 == null ? -1 : playerHistory30.Points,
                            VillagesCount = playerHistory.VillagesCount,
                            VillagesCount24 = playerHistory24 == null ? -1 : playerHistory24.VillagesCount,
                            VillagesCount7 = playerHistory7 == null ? -1 : playerHistory7.VillagesCount,
                            VillagesCount30 = playerHistory30 == null ? -1 : playerHistory30.VillagesCount,
                            Ranking = playerHistory.Ranking,
                            Ranking24 = playerHistory24 == null ? -1 : playerHistory24.Ranking,
                            Ranking7 = playerHistory7 == null ? -1 : playerHistory7.Ranking,
                            Ranking30 = playerHistory30 == null ? -1 : playerHistory30.Ranking,
                            RA = playerHistory.RA,
                            RA24 = playerHistory24 == null ? -1 : playerHistory24.RA,
                            RA7 = playerHistory7 == null ? -1 : playerHistory7.RA,
                            RA30 = playerHistory30 == null ? -1 : playerHistory30.RA,
                            RO = playerHistory.RO,
                            RO24 = playerHistory24 == null ? -1 : playerHistory24.RO,
                            RO7 = playerHistory7 == null ? -1 : playerHistory7.RO,
                            RO30 = playerHistory30 == null ? -1 : playerHistory30.RO,
                            RS = playerHistory.RS,
                            RS24 = playerHistory24 == null ? -1 : playerHistory24.RS,
                            RS7 = playerHistory7 == null ? -1 : playerHistory7.RS,
                            RS30 = playerHistory30 == null ? -1 : playerHistory30.RS,
                        });
                    }
                    else
                    {
                        scope.Error($"History for player {player.Id} - {player.Name} not found");
                    }

                });
                scope.Debug($"Mapped {_players.Count} Players");

                for (int i = 0; i < currentPlayers.Count; i++)
                {
                    PlayerCurrent player = _db.PlayerCurrents.FirstOrDefault(x => x.PlayerId == currentPlayers[i].PlayerId && x.WorldId == currentPlayers[i].WorldId);
                    if (player != null)
                    {
                        player.TribeId = currentPlayers[i].TribeId;
                        player.Points = currentPlayers[i].Points;
                        player.Points7 = currentPlayers[i].Points7;
                        player.Points24 = currentPlayers[i].Points24;
                        player.Points30 = currentPlayers[i].Points30;
                        player.VillagesCount = currentPlayers[i].VillagesCount;
                        player.VillagesCount24 = currentPlayers[i].VillagesCount24;
                        player.VillagesCount7 = currentPlayers[i].VillagesCount7;
                        player.VillagesCount30 = currentPlayers[i].VillagesCount30;
                        player.Ranking = currentPlayers[i].Ranking;
                        player.Ranking24 = currentPlayers[i].Ranking24;
                        player.Ranking7 = currentPlayers[i].Ranking7;
                        player.Ranking30 = currentPlayers[i].Ranking30;
                        player.RA = currentPlayers[i].RA;
                        player.RA24 = currentPlayers[i].RA24;
                        player.RA7 = currentPlayers[i].RA7;
                        player.RA30 = currentPlayers[i].RA30;
                        player.RO = currentPlayers[i].RO;
                        player.RO7 = currentPlayers[i].RO7;
                        player.RO24 = currentPlayers[i].RO24;
                        player.RO30 = currentPlayers[i].RO30;
                        player.RS = currentPlayers[i].RS;
                        player.RS7 = currentPlayers[i].RS7;
                        player.RS24 = currentPlayers[i].RS24;
                        player.RS30 = currentPlayers[i].RS30;
                    }
                    else
                    {
                        _db.PlayerCurrents.Add(currentPlayers[i]);
                    }
                }
                _db.SaveChanges();
                scope.Debug($"Updated {_players.Count} Players");
            }
        }
    }
}
