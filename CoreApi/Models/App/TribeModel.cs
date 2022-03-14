using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Models.App
{
    public class TribeModel
    {
        public TribeModel(string row)
        {
            Id = Convert.ToInt32(row.Split(',')[0]);
            Name = System.Web.HttpUtility.UrlDecode(row.Split(',')[1]);
            Tag = System.Web.HttpUtility.UrlDecode(row.Split(',')[2]);
            Members = Convert.ToInt32(row.Split(',')[3]);
            Villages = Convert.ToInt32(row.Split(',')[4]);
            Points = Convert.ToInt32(row.Split(',')[5]);
            Ranking = Convert.ToInt32(row.Split(',')[7]);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Members { get; set; }
        public int Villages { get; set; }
        public int Points { get; set; }
        public int Ranking { get; set; }
    }
}
