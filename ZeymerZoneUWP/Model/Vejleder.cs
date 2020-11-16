namespace ZeymerZoneWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Vejleder")]
    public partial class Vejleder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vejleder()
        {
            Kalenders = new HashSet<Kalender>();
            Konsultations = new HashSet<Konsultation>();
            Logs = new HashSet<Log>();
        }

        [Key]
        public int Vejleder_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Vejleder_navn { get; set; }

        [Required]
        [StringLength(11)]
        public string Vejleder_tlfnr { get; set; }

        [Required]
        [StringLength(50)]
        public string Vejleder_email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kalender> Kalenders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Konsultation> Konsultations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }
    }
}
