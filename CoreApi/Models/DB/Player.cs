using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreApi.Models.DB
{
    public class Player
    {
        //Keys
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("World")]
        public int WorldId { get; set; }
        //Properties
        public string Name { get; set; }
        public bool Active { get; set; }
        //Virtual Objects
        public virtual World World { get; set; }
        public virtual List<PlayerHistory> History { get; set; }
        public virtual List<VillageHistory> Villages { get; set; }
        public virtual PlayerCurrent Current { get; set; }
        public virtual List<VillageCurrent> VillagesCurrent { get; set; }
    }

    public class PlayerCurrent
    {
        //Keys
        public int Id { get; set; }
        //Foreign Keys
        public int? TribeId { get; set; }
        public int PlayerId { get; set; }
        public int WorldId { get; set; }
        //Properties
        public string Name { get; set; }
        public int Points { get; set; }
        public int Points24 { get; set; }
        public int Points7 { get; set; }
        public int Points30 { get; set; }
        public int VillagesCount { get; set; }
        public int VillagesCount24 { get; set; }
        public int VillagesCount7 { get; set; }
        public int VillagesCount30 { get; set; }
        public int Ranking { get; set; }
        public int Ranking24 { get; set; }
        public int Ranking7 { get; set; }
        public int Ranking30 { get; set; }
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
        //Virtual Objects
        [ForeignKey("TribeId,WorldId")]
        public virtual Tribe Tribe { get; set; }
        [ForeignKey("PlayerId,WorldId")]
        public virtual Player Player { get; set; }
    }

    public class PlayerHistory
    {
        //Keys
        public int Id { get; set; }
        //Foreign Keys
        public int WorldId { get; set; }
        public int PlayerId { get; set; }
        public int? TribeId { get;set; }
        //Properties
        public int Points { get; set; }
        public int VillagesCount { get; set; }
        public int Ranking { get; set; }
        public int RA { get; set; }
        public int RO { get; set; }
        public int RS { get; set; }
        public DateTime Created { get; set; }
        //Virtual Objects
        [ForeignKey("PlayerId,WorldId")]
        public virtual Player Player { get; set; }
        [ForeignKey("TribeId,WorldId")]
        public virtual Tribe Tribe { get; set; }
    }
}
