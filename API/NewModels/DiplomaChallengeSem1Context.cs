using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.NewModels
{
    public partial class DiplomaChallengeSem1Context : DbContext
    {
        public DiplomaChallengeSem1Context()
        {
        }

        public virtual DbSet<Owner> OWNER { get; set; } = null!;
        public virtual DbSet<Pet> PET { get; set; } = null!;
        public virtual DbSet<Procedure> Procedure { get; set; } = null!;
        public virtual DbSet<Treatment> TREATMENT { get; set; } = null!;

        public virtual DbSet<View_Treatment> View_TREATMENT { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-CQVULK8;Initial Catalog=DiplomaChallengeSem1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("OWNER");

                entity.HasIndex(e => e.Phone, "UN_OWNER")
                    .IsUnique();

                entity.Property(e => e.FirstName).HasMaxLength(300);

                entity.Property(e => e.Phone).HasMaxLength(300);

                entity.Property(e => e.Surname).HasMaxLength(300);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("PET");

                entity.HasIndex(e => new { e.OwnerId, e.PetName }, "UN_PetOwner")
                    .IsUnique();

                entity.Property(e => e.PetName).HasMaxLength(300);

                entity.Property(e => e.Type).HasMaxLength(300);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__PET__OwnerId__07970BFE");
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.ToTable("PROCEDURE");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Treatment>()
            .HasKey(t => new { t.Id });

            modelBuilder.Entity<ViewProcedure>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_PROCEDURE");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.PetName).HasMaxLength(300);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<View_Treatment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_TREATMENT");

                entity.Property(e => e.AmountOwed).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FkPetId).HasColumnName("FK_PetId");

                entity.Property(e => e.Notes).HasMaxLength(300);

                entity.Property(e => e.Payment).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PetName).HasMaxLength(300);

                entity.Property(e => e.ProcedureName).HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
