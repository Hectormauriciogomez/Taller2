using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TMecanicoC.Models
{
    public partial class DBTMecanicoCContext : DbContext
    {
        public DBTMecanicoCContext()
        {
        }

        public DBTMecanicoCContext(DbContextOptions<DBTMecanicoCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Personas> Personas { get; set; } = null!;
        public virtual DbSet<Repuestos> Repuestos { get; set; } = null!;
        public virtual DbSet<RevNiveles> RevNiveles { get; set; } = null!;
        public virtual DbSet<Roles> Roles { get; set; } = null!;
        public virtual DbSet<Seguros> Seguros { get; set; } = null!;
        public virtual DbSet<Vehiculos> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__4D7EA8E17112E0AB");

                entity.ToTable("Menu");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK_Menu");
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasKey(e => e.DocIdentificacion)
                    .HasName("PK__Personas__E7DDBB2F52A8EE51");

                entity.Property(e => e.DocIdentificacion)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CiudadDireccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoNivelEstudio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credencial)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles");
            });

            modelBuilder.Entity<Repuestos>(entity =>
            {
                entity.HasKey(e => e.IdRepuestos)
                    .HasName("PK__Repuesto__AA4652AB3BDE658D");

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.Justificacion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PlacaNavigation)
                    .WithMany(p => p.Repuestos)
                    .HasForeignKey(d => d.Placa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiculosR");
            });

            modelBuilder.Entity<RevNiveles>(entity =>
            {
                entity.HasKey(e => e.IdRevNiveles)
                    .HasName("PK__RevNivel__1F035DE220C607AE");

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.NivelAceite).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.NivelDireccion).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.NivelFrenos).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.NivelRefrigerante).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Placa)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.PlacaNavigation)
                    .WithMany(p => p.RevNiveles)
                    .HasForeignKey(d => d.Placa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehiculosRN");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Roles__2A49584C3BF28A8D");

                entity.Property(e => e.Autorizacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Seguros>(entity =>
            {
                entity.HasKey(e => e.IdSeguros)
                    .HasName("PK__Seguros__A0A46E3B96457752");

                entity.Property(e => e.FechaCompra).HasColumnType("datetime");

                entity.Property(e => e.FechaVence).HasColumnType("datetime");

                entity.Property(e => e.Placa)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PlacaNavigation)
                    .WithMany(p => p.Seguros)
                    .HasForeignKey(d => d.Placa)
                    .HasConstraintName("FK_VehiculosS");
            });

            modelBuilder.Entity<Vehiculos>(entity =>
            {
                entity.HasKey(e => e.Placa)
                    .HasName("PK__Vehiculo__8310F99C62139734");

                entity.Property(e => e.Placa)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CapacidadPasajeros)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CilindradaMotor)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DocIdMecanico)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DocIdPropietario)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OtrasCaracteristicas)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaisOrigen)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.DocIdMecanicoNavigation)
                    .WithMany(p => p.VehiculoDocIdMecanicoNavigations)
                    .HasForeignKey(d => d.DocIdMecanico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mecanico");

                entity.HasOne(d => d.DocIdPropietarioNavigation)
                    .WithMany(p => p.VehiculoDocIdPropietarioNavigations)
                    .HasForeignKey(d => d.DocIdPropietario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Propietario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
