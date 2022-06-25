// namespace API;

// using API.Models;
// using Microsoft.EntityFrameworkCore;

// public class PetContext: DbContext {
//      protected readonly IConfiguration Configuration;

//         public PetContext(IConfiguration configuration)
//     {
//         Configuration = configuration;
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder options)
//     {
//         // connect to sql server with connection string from app settings
//         options.UseSqlServer(Configuration["DB"]);
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Owner>()
//          .HasKey(o => new { o.OwnerID });

//         modelBuilder.Entity<Pet>()
//          .HasKey(o => new { o.ID });

//         modelBuilder.Entity<Procedure>()
//          .HasKey(o => new { o.ID });

//         modelBuilder.Entity<View_Treatment>()
//         .HasNoKey();
//     }
//     public virtual DbSet<Owner> OWNER { get; set; } = null!;
//     public virtual DbSet<Pet> PET { get; set; } = null!;
//     public virtual DbSet<View_Treatment> View_TREATMENT { get; set; } = null!;
//     public virtual DbSet<Procedure> Procedure { get; set; } = null!;
// }