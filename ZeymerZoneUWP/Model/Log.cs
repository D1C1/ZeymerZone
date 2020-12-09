namespace ZeymerZoneUWP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Log
    {
        [Key]
        public int Logs_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Overskrift { get; set; }

        [Column("Log")]
        [Required]
        [StringLength(255)]
        public string Log1 { get; set; }

        public int Kunde_Id { get; set; }

        public int Vejleder_Id { get; set; }

        


        [Column(TypeName = "date")]
        public DateTime Log_date { get; set; }

        public virtual Kunde Kunde { get; set; }

        public virtual Vejleder Vejleder { get; set; }

        public int Kunde_vaegt_dd { get; set; }
    }
}
