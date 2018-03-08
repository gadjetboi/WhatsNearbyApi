using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WhatsNearbyApi.Entities
{
    public class WhatsNearbyApiContext : IdentityDbContext
    {  
        public WhatsNearbyApiContext(DbContextOptions<WhatsNearbyApiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            //TODO: Implement Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = "decimal(18, 5)";
            }
        }

        public DbSet<CustomUser> CustomUsers { get; set; }
    }

}
