using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class DBContextWrite(DbContextOptions<DBContextWrite> options) : DbContext(options)
    {
        DbSet<InventoryItem> InventoryItems { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContextWrite).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
