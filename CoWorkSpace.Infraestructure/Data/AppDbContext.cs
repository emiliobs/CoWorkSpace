using CoWorkSpace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorkSpace.Infraestructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Space> Spaces { get; set; }
    public DbSet<SpaceAmenity> SpaceAmenities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
        }).Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TransactionId).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Reservation)
                  .WithMany() // Assuming Reservation doesn't have a navigation property back to Payment
                  .HasForeignKey(e => e.ReservationId)
                  .OnDelete(DeleteBehavior.Restrict); // Or Cascade, depending on business logic
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Space)
                  .WithMany()
                  .HasForeignKey(e => e.SpaceId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ImageUrl).HasMaxLength(500); // Assuming a URL can be long
        });

        modelBuilder.Entity<SpaceAmenity>(entity =>
        {
            entity.HasKey(e => e.Id);
            // Define foreign key relationships
            entity.HasOne(sa => sa.Space)
                  .WithMany() // Assuming Space doesn't have a navigation property back to SpaceAmenity
                  .HasForeignKey(sa => sa.SpaceId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(sa => sa.Amenity)
                  .WithMany() // Assuming Amenity doesn't have a navigation property back to SpaceAmenity
                  .HasForeignKey(sa => sa.AmenityId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
        });
    }
}