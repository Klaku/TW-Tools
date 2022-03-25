using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using CoreApi.Tasks;
namespace Engine
{
    public class HangfireService : IDisposable
    {
        private BackgroundJobServer _server;

        public HangfireService()
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=DESKTOP-AEK8MHR\\SQLEXPRESS;Database=Hangfire;Integrated Security=SSPI;");
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
