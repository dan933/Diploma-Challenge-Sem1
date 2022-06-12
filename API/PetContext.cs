namespace API;

using Microsoft.EntityFrameworkCore;
using models;

public class PetContext: DbContext {
     protected readonly IConfiguration Configuration;

        public PetContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        options.UseSqlServer(Configuration.GetConnectionString("DB"));
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Owner>()
        .HasKey(o => new { o.OwnerId });

        modelBuilder.Entity<Pet>()
        .HasKey(p => new { p.OwnerId, p.PetName });

        modelBuilder.Entity<Treatment>()
        .HasNoKey();

        modelBuilder.Entity<ProcedureView>()
        .HasNoKey();


    }

    public virtual DbSet<Owner> Owner { get; set; } = null!;
    public virtual DbSet<Pet> Pet { get; set; } = null!;
    public virtual DbSet<Treatment> Treatment { get; set; } = null!;

    public virtual DbSet<ProcedureView> view_procedure { get; set; } = null!;
}