using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProIcbf.Models;

public partial class CopyIcbfContext : DbContext
{
    public CopyIcbfContext()
    {
    }

    public CopyIcbfContext(DbContextOptions<CopyIcbfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Jardine> Jardines { get; set; }

    public virtual DbSet<Nino> Ninos { get; set; }

    public virtual DbSet<ReporteAcademico> ReportesAcademicos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__Asistenc__20240AC49FE4972E");

            entity.Property(e => e.IdAsistencia).HasColumnName("Id_Asistencia");
            entity.Property(e => e.Detalles).HasColumnType("text");
            entity.Property(e => e.IdNino).HasColumnName("Id_Nino");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

            entity.HasOne(d => d.oNino).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdNino)
                .HasConstraintName("FK__Asistenci__Id_Ni__571DF1D5");

            entity.HasOne(d => d.oUsuario).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Asistenci__Id_Us__5812160E");
        });

        modelBuilder.Entity<Jardine>(entity =>
        {
            entity.HasKey(e => e.IdJardin).HasName("PK__Jardines__768CE4F90B077FB1");

            entity.HasIndex(e => e.NombreJardin, "UQ__Jardines__0804982D84817670").IsUnique();

            entity.Property(e => e.IdJardin).HasColumnName("Id_Jardin");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.NombreJardin)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.oUsuario).WithMany(p => p.Jardines)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Jardines__Id_Usu__59063A47");
        });

        modelBuilder.Entity<Nino>(entity =>
        {
            entity.HasKey(e => e.IdNino).HasName("PK__Ninos__D960C01D6DE95151");

            entity.Property(e => e.IdNino).HasColumnName("Id_Nino");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Eps)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EPS");
            entity.Property(e => e.IdJardin).HasColumnName("Id_Jardin");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sangre)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.oJardin).WithMany(p => p.Ninos)
                .HasForeignKey(d => d.IdJardin)
                .HasConstraintName("FK__Ninos__Id_Jardin__5AEE82B9");

            entity.HasOne(d => d.oUsuario).WithMany(p => p.Ninos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Ninos__Id_Usuari__59FA5E80");
        });

        modelBuilder.Entity<ReporteAcademico>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("PK__Reportes__6ED7B73A734564A4");

            entity.Property(e => e.IdReporte).HasColumnName("Id_Reporte");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.IdNino).HasColumnName("Id_Nino");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Nivel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Notas)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.oNino).WithMany(p => p.ReportesAcademicos)
                .HasForeignKey(d => d.IdNino)
                .HasConstraintName("FK__ReportesA__Id_Ni__5BE2A6F2");

            entity.HasOne(d => d.oUsuario).WithMany(p => p.ReportesAcademicos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__ReportesA__Id_Us__5CD6CB2B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__55932E86A6BAC8EF");

            entity.HasIndex(e => e.NombreRol, "UQ__Roles__4F0B537F193F26A9").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__63C76BE25BEFA7F6");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE094D1BBFD").IsUnique();

            entity.HasIndex(e => e.Cedula, "UQ__Usuarios__B4ADFE3847840D2F").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.oRol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuarios__Id_Rol__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
