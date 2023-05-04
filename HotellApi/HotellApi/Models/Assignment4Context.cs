using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotellApi.Models;

public partial class Assignment4Context : DbContext
{
    public Assignment4Context()
    {
    }

    public Assignment4Context(DbContextOptions<Assignment4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomService> RoomServices { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:dat154shared.database.windows.net,1433;Initial Catalog=Assignment4;Persist Security Info=False;User ID=azureUser;Password=Mitt1.passord;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.Property(e => e.ReservationId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Customer");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Room");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomNumber);

            entity.ToTable("Room");

            entity.Property(e => e.RoomNumber).ValueGeneratedNever();
            entity.Property(e => e.NeedService).HasMaxLength(50);
        });

        modelBuilder.Entity<RoomService>(entity =>
        {
            entity.ToTable("RoomService");

            entity.Property(e => e.RoomServiceId).ValueGeneratedNever();

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.RoomServices)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomService_Room");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.StaffId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
