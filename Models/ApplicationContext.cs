using Microsoft.EntityFrameworkCore;
namespace dotnet_bakery.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        
        // Add our own models to the ApplicationContext so that they are database-aware
        // This will create a table called Bakers ( capital b), with all the columns 
        // that are found in the Baker class. This is the full link between our pure C#
        // model in Baker.cs and the magoc of translation to Postgres.
        public DbSet<Baker> Bakers { get; set; }
        public DbSet<BreadInventory> BreadInventory { get; set; }
    }
}