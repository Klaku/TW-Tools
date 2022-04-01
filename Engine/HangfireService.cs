using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using CoreApi.Tasks;
using CoreApi.Models.DB;
namespace Engine
{
    public class HangfireService : IDisposable
    {
        private BackgroundJobServer _server;

        public HangfireService()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.LocalHangfireDatabase)[0]);
            _server = new BackgroundJobServer();
        }

        public void Reinstall()
        {
            DataMiner miner = new DataMiner();
            RecurringJob.AddOrUpdate("Get Data", () => miner.Process(), "5 * * * *");
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
