using Heavy.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Heavy.Web.Data
{
    public class AlbumConfiguration: IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Artist).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ReleaseDate).HasColumnType("date");
            builder.Property(x => x.Price).HasColumnType("decimal(6,2)");
            builder.Property(x => x.CoverUrl).IsRequired().HasMaxLength(200);
        }
    }
}
