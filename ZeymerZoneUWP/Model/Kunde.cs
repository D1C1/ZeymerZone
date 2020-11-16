namespace ZeymerZoneUWP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Kunde")]
    public partial class Kunde
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kunde()
        {
            Konsultations = new HashSet<Konsultation>();
            Kostplans = new HashSet<Kostplan>();
            Logs = new HashSet<Log>();
        }

        [Key]
        public int Kunde_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Kunde_navn { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(11)]
        public string Kunde_tlfnr { get; set; }

        [Required]
        [StringLength(50)]
        public string Kunde_vejNavn { get; set; }

        public int Kunde_postnr { get; set; }

        [Column(TypeName = "date")]
        public DateTime Kunde_fødeselsdag { get; set; }

        public int Kunde_vægt { get; set; }

        public int Kunde_højde { get; set; }

        [Required]
        [StringLength(50)]
        public string Kunde_email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Konsultation> Konsultations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kostplan> Kostplans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Logs { get; set; }
    }
}
