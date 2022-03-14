using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Models.App
{
    public class StatModel
    {
        public StatModel(string row)
        {
            Rank = Convert.ToInt32(row.Split(',')[0]);
            Id = Convert.ToInt32(row.Split(',')[1]);
            Score = Convert.ToInt32(row.Split(',')[2]);
        }

        public int Rank { get; set; }
        public int Id { get; set; }
        public int Score { get; set; }
    }
}
