using System;

namespace WakeUpTool
{
    public static class Program
    {
        static void Main()
        {
            var tool = new Tool();
            tool.Start().Wait();
        }
    }

    public class Tool
    {
        public HttpClient _client;

        public Tool()
        {
            _client = new HttpClient();
        }

        public async Task Start()
        {
            while (true)
            {
                var response = await _client.GetAsync("http://dashboard.dev.pl/hangfire/");
                Console.WriteLine($"{DateTime.Now} Response recieved, Code: {response.StatusCode}");
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
    }
}