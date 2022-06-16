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
            //Konfiguracja serwera
            GlobalConfiguration.Configuration.UseSqlServerStorage(CustomContextFactory.ContextOf(CustomContextFactory.ConnectionStrings.LocalHangfireDatabase)[0]);
            //Utworzenie instancji serwera
            _server = new BackgroundJobServer();
        }

        public void Reinstall()
        {
            //Utworzenie instancji klasy DataMiner
            DataMiner miner = new DataMiner();
            //Dodanie lub aktualizacja funkcji z kluczem "Get Data" 
            //Oraz ustalenie interwału wykonywania na piątą minutę każdej z godzin podzielnych przez 4
            RecurringJob.AddOrUpdate("Get Data", () => miner.Process(), "5 */4 * * *");
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
