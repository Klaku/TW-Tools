using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Models.App
{
    public class VillageModel
    {
        public VillageModel(string row)
        {
            Id = Convert.ToInt32(row.Split(',')[0]);
            Name = System.Web.HttpUtility.UrlDecode(row.Split(',')[1]);
            X = Convert.ToInt32(row.Split(',')[2]);
            Y = Convert.ToInt32(row.Split(',')[3]);
            PlayerId = Convert.ToInt32(row.Split(',')[4]);
            Points = Convert.ToInt32(row.Split(',')[5]);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PlayerId { get; set; }
        public int Points { get; set; }
    }
}
