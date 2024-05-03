using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.Core.Entities;


namespace PlaceRentalApp.Infraestructure.Persistence
{
    public class PlaceRentalDbContext :DbContext
    {
        public PlaceRentalDbContext(DbContextOptions<PlaceRentalDbContext> options) : base(options)
        {
            
        }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceAmenity> PlaceAmenities { get; set; }
        public DbSet<PlaceBook> PlaceBooks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>(x => {
                x.HasKey(c => c.Id);

                x.HasMany(p => p.Amenities)
                .WithOne()
                .HasForeignKey(p => p.IdPlace)
                .OnDelete(DeleteBehavior.Restrict);

                x.HasMany(b => b.Books)
                .WithOne(p => p.Place)
                .HasForeignKey(p => p.IdPlace)
                .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(u => u.User)
                .WithMany(p => p.Places)
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

                x.OwnsOne(e => e.Address, a =>
                {
                    a.Property(d => d.Street).HasColumnName("Street");
                    a.Property(d => d.Number).HasColumnName("Number");
                    a.Property(d => d.ZipCode).HasColumnName("ZipCode");
                    a.Property(d => d.District).HasColumnName("District");
                    a.Property(d => d.City).HasColumnName("City");
                    a.Property(d => d.State).HasColumnName("State");
                    a.Property(d => d.Country).HasColumnName("Country");
                });
            });
            modelBuilder.Entity<PlaceAmenity>(x => {
                x.HasKey(c => c.Id);
            });
            modelBuilder.Entity<PlaceBook>(x => {
                x.HasKey(c => c.Id);
            });
            modelBuilder.Entity<User>(x => {
                x.HasKey(c => c.Id);

                x.HasMany(b => b.Books)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
