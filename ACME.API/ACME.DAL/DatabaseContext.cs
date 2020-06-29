using Microsoft.EntityFrameworkCore;

namespace ACME.DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Applications> Applications { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Postcodes> Postcodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applications>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applications_Country");

                entity.HasOne(d => d.Postcode)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.PostcodeId)
                    .HasConstraintName("FK_Applications_Postcodes");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Postcodes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bspname)
                    .HasColumnName("BSPname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bspnumber)
                    .HasColumnName("BSPnumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryOffice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Locality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParcelZone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreSortIndicator)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
