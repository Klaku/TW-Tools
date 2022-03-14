using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Models.App
{
    public class PlayerModel
    {
        public PlayerModel(string row)
        {
            Id = Convert.ToInt32(row.Split(',')[0]);
            Name = System.Web.HttpUtility.UrlDecode(row.Split(',')[1]);
            TribeId = Convert.ToInt32(row.Split(',')[2]);
            Villages = Convert.ToInt32(row.Split(',')[3]);
            Points = Convert.ToInt32(row.Split(',')[4]);
            Rank = Convert.ToInt32(row.Split(',')[5]);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TribeId { get; set; }
        public int Villages { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }
    }
}
