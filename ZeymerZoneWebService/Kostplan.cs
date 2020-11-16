namespace ZeymerZoneWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kostplan")]
    public partial class Kostplan
    {
        [Key]
        public int Kostplan_Id { get; set; }

        public int Kunde_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Opskriftnavn { get; set; }

        [Required]
        [StringLength(50)]
        public string Ugedag { get; set; }

        [StringLength(50)]
        public string Link { get; set; }

        public virtual Kunde Kunde { get; set; }
    }
}
