using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Test
{
    public partial class DiplomaChallengeSem1Context : DbContext
    {
        public DiplomaChallengeSem1Context()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CQVULK8;Initial Catalog=DiplomaChallengeSem1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
            .HasKey(o => new { o.OwnerId });

            modelBuilder.Entity<Pet>()
            .HasKey(p => new { p.Id });

            modelBuilder.Entity<Procedure>()
            .HasKey(pr => new { pr.ProcedureId });

            modelBuilder.Entity<Treatment>()
            .HasKey(tr => new { tr.Id });

            modelBuilder.Entity<ViewTreatment>()
            .HasNoKey();

            modelBuilder.Entity<ViewProcedure>()
            .HasNoKey();

        }

        public virtual DbSet<Owner> OWNER { get; set; } = null!;
        public virtual DbSet<Pet> PET { get; set; } = null!;
        public virtual DbSet<Procedure> PROCEDURE { get; set; } = null!;
        public virtual DbSet<Treatment> TREATMENT { get; set; } = null!;
        public virtual DbSet<ViewProcedure> view_PROCEDURE { get; set; } = null!;
        public virtual DbSet<ViewTreatment> view_TREATMENT { get; set; } = null!;
    }
}
