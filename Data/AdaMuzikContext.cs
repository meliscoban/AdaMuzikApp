using AdaMuzik.Models;
using Microsoft.EntityFrameworkCore;

namespace AdaMuzik.Data
{
    public class AdaMuzikContext : DbContext
    {
        public AdaMuzikContext()
        {
        }

        public AdaMuzikContext(DbContextOptions<AdaMuzikContext> options) : base(options)
        {
        }

        public DbSet<Sanatci> Sanatcilar { get; set; }
        public DbSet<Album> Albumler { get; set; }
        public DbSet<Sarki> Sarkilar { get; set; }
        public DbSet<CalmaListesi> CalmaListeleri { get; set; }
        public DbSet<CalmaListesiSarkisi> CalmaListeleriSarkilari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Sanatci)
                .WithMany(s => s.Albumler)
                .HasForeignKey(a => a.SanatciId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sarki>()
                .HasOne(s => s.Sanatci)
                .WithMany(san => san.Sarkilar)
                .HasForeignKey(s => s.SanatciId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sarki>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Sarkilar)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CalmaListesiSarkisi>()
                .HasOne(cls => cls.CalmaListesi)
                .WithMany(cl => cl.CalmaListeleriSarkilari)
                .HasForeignKey(cls => cls.CalmaListesiId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CalmaListesiSarkisi>()
                .HasOne(cls => cls.Sarki)
                .WithMany(s => s.CalmaListeleriSarkilari)
                .HasForeignKey(cls => cls.SarkiId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
