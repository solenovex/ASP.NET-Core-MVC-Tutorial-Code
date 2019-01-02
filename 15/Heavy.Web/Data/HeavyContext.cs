using Heavy.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Heavy.Web.Data
{
    public class HeavyContext : DbContext
    {
        public HeavyContext(DbContextOptions<HeavyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        }

        public DbSet<Album> Albums { get; set; }
    }
}
