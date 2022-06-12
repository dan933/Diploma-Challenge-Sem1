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

    public virtual DbSet<Owner> Owner { get; set; } = null!;
}