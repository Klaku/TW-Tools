using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Models.DB
{
    public class CustomContext : DbContext
    {
        public CustomContext(DbContextOptions<CustomContext> options) : base(options)
        {
            
        }

        public DbSet<Village> Village { get; set; }
        public DbSet<VillageHistory> VillageHistory { get; set; }
        public DbSet<VillageCurrent> VillageCurrent { get; set;}
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerHistory> PlayerHistory { get; set; }
        public DbSet<PlayerCurrent> PlayerCurrents { get; set; }
        public DbSet<Tribe> Tribe { get; set; }
        public DbSet<TribeHistory> TribeHistory { get; set; }
        public DbSet<TribeCurrent> TribeCurrent { get; set; }
        public DbSet<World> World { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasKey(o => new { o.Id, o.WorldId });
            modelBuilder.Entity<Tribe>()
                .HasKey(o => new {o.Id, o.WorldId});
            modelBuilder.Entity<Village>()
                .HasKey(o => new { o.Id, o.WorldId });
        }

    }

    

    public class CustomContextFactory : IDesignTimeDbContextFactory<CustomContext>
    {
        public CustomContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomContext>();
            optionsBuilder.UseSqlServer(args[0]);

            return new CustomContext(optionsBuilder.Options);
        }

        public static string[] ContextOf(ConnectionStrings value)
        {
            switch (value)
            {
                case ConnectionStrings.AzureDatabase:
                    return new[] { "#########################" };
                case ConnectionStrings.LocalDatabase:
                    return new[] { "#########################" };
                case ConnectionStrings.LocalHangfireDatabase:
                    return new[] { "#########################" };
                default: 
                    return new[] { "" };
            }
        }

        public enum ConnectionStrings
        {
            AzureDatabase =0,
            LocalDatabase = 1,
            LocalHangfireDatabase = 2
        }
    }
}
