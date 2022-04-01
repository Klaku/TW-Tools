using CoreApi.Helpers;
using CoreApi.Models.App;
using CoreApi.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Tasks
{
    public class RemoveEntitiesTask
    {
        public static int Tribes(List<TribeModel> tribes, World world)
        {
            using (CustomContext context = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase)))
            {
                int result = 0;
                List<Tribe> entities = context.Tribe.Where(t => t.WorldId == world.Id && t.Active == true).ToList();
                entities.ForEach(entity => {
                    if(!tribes.Any(x => x.Id == entity.Id))
                    {
                        entity.Active = false;
                        context.Tribe.Update(entity);
                        result++;
                    }
                });
                context.SaveChanges();

                return result;
            }
        }

        public static int Players(List<PlayerModel> players, World world)
        {
            using (CustomContext context = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase)))
            {
                int result = 0;
                List<Player> entities = context.Player.Where(t => t.WorldId == world.Id && t.Active == true).ToList();
                entities.ForEach(entity => {
                    if (!players.Any(x => x.Id == entity.Id))
                    {
                        entity.Active = false;
                        context.Player.Update(entity);
                        result++;
                    }
                });
                context.SaveChanges();
                return result;
            }
        }

        public static int Villages(List<VillageModel> villages, World world)
        {
            using (CustomContext context = new CustomContextFactory().CreateDbContext(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.AzureDatabase)))
            {
                int result = 0;
                List<Village> entities = context.Village.Where(t => t.WorldId == world.Id && t.Active == true).ToList();
                entities.ForEach(entity => {
                    if (!villages.Any(x => x.Id == entity.Id))
                    {
                        entity.Active = false;
                        context.Village.Update(entity);
                        result++;
                    }
                });
                context.SaveChanges();
                return result;
            }
        }

    }
}
