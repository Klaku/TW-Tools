using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Models.DB
{
    public class World
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string SubDomain { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Player> Players { get; set; }
        public virtual List<Tribe> Tribes { get; set; }
        public virtual List<Village> Villages { get; set; }
    }
}
