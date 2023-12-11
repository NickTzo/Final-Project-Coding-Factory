using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingAppApi.Data;

public partial class BookingAppApiDbContext : DbContext
{

    public BookingAppApiDbContext(DbContextOptions<BookingAppApiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CARS__3214EC275B16CD1E");

            entity.ToTable("CARS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.IsVisible).HasColumnName("IS_VISIBLE");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("BRAND");
            entity.Property(e => e.Cc)
                .HasMaxLength(50)
                .HasColumnName("CC");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("MODEL");
            entity.Property(e => e.Seat)
                .HasMaxLength(50)
                .HasColumnName("SEAT");
            entity.Property(e => e.Year)
               .HasMaxLength(50)
               .HasColumnName("YEAR");
            entity.Property(e => e.Transmission)
                .HasMaxLength(50)
                .HasColumnName("TRANSMISSION");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(50)
                .HasColumnName("PHOTO_URL");
            entity.Property(e => e.PhotoId)
                .HasMaxLength(50)
                .HasColumnName("PHOTO_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
           
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESERVAT__3214EC27486CE380");

            entity.ToTable("RESERVATIONS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarId).HasColumnName("CAR_ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("ENDING_DATE");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("STARTING_DATE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Car).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK_RESERVATIONS_TO_CARS");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RESERVATIONS_TO_USERS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC27450F6C37");

            entity.ToTable("USERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("PHONE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
            entity.Property(e => e.IsOwner).HasColumnName("IS_OWNER");
                
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
