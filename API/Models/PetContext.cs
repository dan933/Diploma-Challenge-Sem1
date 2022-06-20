namespace API;

using API.Models;
using Microsoft.EntityFrameworkCore;

public class PetContext: DbContext {
     protected readonly IConfiguration Configuration;

        public PetContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server with connection string from app settings
        options.UseSqlServer(Configuration["DB"]);
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Owner>()
        .HasKey(o => new { o.OwnerID });
    }
    public virtual DbSet<Owner> OWNER { get; set; } = null!;
}