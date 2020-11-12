using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ZeymerZoneWebService
{
    public partial class ZZDBContext : DbContext
    {
        public ZZDBContext()
            : base("name=ZZDBContext")
        {
        }

        public virtual DbSet<Kalender> Kalenders { get; set; }
        public virtual DbSet<Konsultation> Konsultations { get; set; }
        public virtual DbSet<Kostplan> Kostplans { get; set; }
        public virtual DbSet<Kunde> Kundes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Vejleder> Vejleders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kostplan>()
                .Property(e => e.Opskriftnavn)
                .IsUnicode(false);

            modelBuilder.Entity<Kostplan>()
                .Property(e => e.Ugedag)
                .IsUnicode(false);

            modelBuilder.Entity<Kostplan>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Kunde>()
                .Property(e => e.Kunde_navn)
                .IsUnicode(false);

            modelBuilder.Entity<Kunde>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Kunde>()
                .Property(e => e.Kunde_tlfnr)
                .IsUnicode(false);

            modelBuilder.Entity<Kunde>()
                .Property(e => e.Kunde_vejNavn)
                .IsUnicode(false);

            modelBuilder.Entity<Kunde>()
                .Property(e => e.Kunde_email)
                .IsUnicode(false);

            modelBuilder.Entity<Kunde>()
                .HasMany(e => e.Konsultations)
                .WithRequired(e => e.Kunde)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kunde>()
                .HasMany(e => e.Kostplans)
                .WithRequired(e => e.Kunde)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kunde>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Kunde)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Overskrift)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Log1)
                .IsUnicode(false);

            modelBuilder.Entity<Vejleder>()
                .Property(e => e.Vejleder_navn)
                .IsUnicode(false);

            modelBuilder.Entity<Vejleder>()
                .Property(e => e.Vejleder_tlfnr)
                .IsUnicode(false);

            modelBuilder.Entity<Vejleder>()
                .Property(e => e.Vejleder_email)
                .IsUnicode(false);

            modelBuilder.Entity<Vejleder>()
                .HasMany(e => e.Kalenders)
                .WithRequired(e => e.Vejleder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vejleder>()
                .HasMany(e => e.Konsultations)
                .WithRequired(e => e.Vejleder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vejleder>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Vejleder)
                .WillCascadeOnDelete(false);
        }
    }
}
