using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApi.Models.DB;
using CoreApi.Helpers;
using NLog;
using System.Net.Http;

namespace CoreApi.Requests
{
    public class Request
    {
        public static async Task<List<string>> Fetch(World world, string path, MonitoredScope scope)
        {
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync($"https://{world.SubDomain}.{world.Domain}{path}");

                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();

                return body.Split('\n').ToList();
            }
            catch (Exception e)
            {
                scope.Error(e.Message);
                return new List<string>();
            }
        }
    }
}
