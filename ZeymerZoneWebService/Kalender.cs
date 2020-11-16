namespace ZeymerZoneUWP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ZeymerZoneWebService;

    [Table("Kalender")]
    public partial class Kalender
    {
        [Key]
        public int Kalender_Id { get; set; }

        public int Vejleder_Id { get; set; }

        public DateTime CalenderDate { get; set; }

        public virtual Vejleder Vejleder { get; set; }
    }
}
