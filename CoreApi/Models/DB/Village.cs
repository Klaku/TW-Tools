using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreApi.Models.DB
{
    public class Village
    {
        //Keys
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("World")]
        public int WorldId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        //Virtual Objects
        public virtual World World { get; set; }
        public virtual List<VillageHistory> History { get; set; }
        public virtual VillageCurrent Current { get; set; }
    }

    public class VillageCurrent
    {
        //Keys
        public int Id { get; set; }
        //Properties
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Points { get; set; }
        public int Points7 { get; set; }
        public int Points30 { get; set; }
        //Foreign Keys
        public int? TribeId { get; set; }
        public int WorldId { get; set; }
        public int VillageId { get; set; }
        public int? PlayerId { get; set; }
        //Virtual Objects
        [ForeignKey("TribeId,WorldId")]
        public virtual Tribe Tribe { get; set; }
        [ForeignKey("VillageId,WorldId")]
        public virtual Village Village { get; set; }
        [ForeignKey("PlayerId,WorldId")]
        public virtual Player Player { get; set; }
    }

    public class VillageHistory
    {
        //Keys
        public int Id { get; set; }
        //Properties
        public int Points { get; set; }
        public DateTime Created { get; set; }
        //Foreign Keys
        public int WorldId { get; set; }
        public int VillageId { get; set; }
        public int? PlayerId { get; set; }
        //Virtual Objects
        [ForeignKey("VillageId,WorldId")]
        public virtual Village Village { get; set; }
        [ForeignKey("PlayerId,WorldId")]
        public virtual Player Player { get; set; }
    }
}
