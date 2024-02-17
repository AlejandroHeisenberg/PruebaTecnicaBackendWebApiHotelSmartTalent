using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiHotel.Models;

public partial class HotelBdContext : DbContext
{
    public HotelBdContext()
    {
    }

    public HotelBdContext(DbContextOptions<HotelBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContactosEmergencium> ContactosEmergencia { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<Hotele> Hoteles { get; set; }

    public virtual DbSet<Pasajero> Pasajeros { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=HotelBD; Trusted_Connection=True; trustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactosEmergencium>(entity =>
        {
            entity.HasKey(e => e.IdContactoEmergencia).HasName("PK__Contacto__3A12A766F56FDC5F");

            entity.Property(e => e.IdContactoEmergencia).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Nombres)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.ContactosEmergencia)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contactos__IdRes__4F7CD00D");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF901B62F055B");

            entity.Property(e => e.IdHabitacion).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Activa).HasDefaultValue(true);
            entity.Property(e => e.CostoBase).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Impuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdHot__3E52440B");
        });

        modelBuilder.Entity<Hotele>(entity =>
        {
            entity.HasKey(e => e.IdHotel).HasName("PK__Hoteles__653BCCC456F6F893");

            entity.Property(e => e.IdHotel).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Ciudad)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pasajero>(entity =>
        {
            entity.HasKey(e => e.IdPasajero).HasName("PK__Pasajero__78E232CB80887D68");

            entity.Property(e => e.IdPasajero).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Genero)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Pasajeros)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pasajeros__IdRes__4BAC3F29");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__0E49C69DEC02F8CE");

            entity.Property(e => e.IdReserva).HasDefaultValueSql("(newid())");
            entity.Property(e => e.EmailContacto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaEntrada).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__IdHabi__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
