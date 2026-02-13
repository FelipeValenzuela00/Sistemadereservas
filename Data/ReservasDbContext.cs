using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.Data;

public partial class ReservasDbContext : DbContext
{
    public ReservasDbContext()
    {
    }

    public ReservasDbContext(DbContextOptions<ReservasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioEmail> UsuarioEmails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IO0VI6T; Database=ReservasDB; Trusted_Connection=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.RecursoId).HasName("PK__Recursos__82F2B184B86AF0C5");

            entity.Property(e => e.Capacidad).HasDefaultValue(1);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservasId).HasName("PK__Reservas__5A355F4CA36A6540");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Activa");

            entity.HasOne(d => d.Recurso).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.RecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservas_Recursos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservas_Usuarios");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F16D03A76B");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCFA631BFE7").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B8A5C8073F");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_ROLES");
        });

        modelBuilder.Entity<UsuarioEmail>(entity =>
        {
            entity.HasKey(e => e.EmailId).HasName("PK__UsuarioE__7ED91AEFD702D79C");

            entity.HasIndex(e => e.Email, "UQ__UsuarioE__A9D1053447D1C76E").IsUnique();

            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EsPrincipal).HasDefaultValue(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioEmails)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Emails_Usuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
