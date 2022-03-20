using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreApi.Models.DB
{
    public class Tribe
    {
        //Keys
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("World")]
        public int WorldId { get; set; }
        //Properties 
        public bool Active { get; set; }
        //Virtual Objects
        public virtual World World { get; set; }
        public virtual List<TribeHistory> History { get; set; }
        public virtual List<PlayerHistory> Players { get; set; }
        public virtual TribeCurrent Current { get; set; }
        public virtual List<VillageCurrent> Villages { get; set; }
        public virtual List<PlayerCurrent> CurrentPlayers { get; set; }
    }

    public class TribeCurrent
    {
        //Keys
        public int Id { get; set; }
        //Foreign Keys
        public int WorldId { get; set; }
        public int TribeId { get; set; }
        //Properties
        public string Name { get; set; }
        public string Tag { get; set; }
        public int Villages { get; set; }
        public int Villages24 { get; set; }
        public int Villages7 { get; set; }
        public int Villages30 { get; set; }
        public int Points { get; set; }
        public int Points24 { get; set; }
        public int Points7 { get; set; }
        public int Points30 { get; set; }
        public int RA { get; set; }
        public int RA24 { get; set; }
        public int RA7 { get; set; }
        public int RA30 { get; set; }
        public int RO { get; set; }
        public int RO24 { get; set; }
        public int RO7 { get; set; }
        public int RO30 { get; set; }
        public int RS { get; set; }
        public int RS24 { get; set; }
        public int RS7 { get; set; }
        public int RS30 { get; set; }
        public int Ranking { get; set; }
        public int Ranking24 { get; set; }
        public int Ranking7 { get; set; }
        public int Ranking30 { get; set; }
        //Virtual Objects
        [ForeignKey("TribeId,WorldId")]
        public virtual Tribe Tribe { get; set; }
    }

    public class TribeHistory
    {
        //Keys
        public int Id { get; set; }
        //Foreign Keys
        public int WorldId { get; set; }
        public int TribeId { get; set; }
        //Properties
        public string Name { get; set; }
        public string Tag { get; set; }
        public int RA { get; set; }
        public int RO { get; set; }
        public int RS { get; set; }
        public int Ranking { get; set; }
        public int Points { get; set; }
        public int Villages { get; set; }
        public DateTime Created { get; set; }
        //Virtual Objects
        [ForeignKey("TribeId,WorldId")]
        public virtual Tribe Tribe { get; set; }
    }

}
