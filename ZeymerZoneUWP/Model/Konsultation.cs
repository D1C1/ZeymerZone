namespace ZeymerZoneWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Konsultation")]
    public partial class Konsultation
    {
        [Key]
        public int Konsultation_Id { get; set; }

        public int Vejleder_Id { get; set; }

        public int Kunde_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Konsultation_date { get; set; }

        public virtual Kunde Kunde { get; set; }

        public virtual Vejleder Vejleder { get; set; }
    }
}
