using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BaseDatosMusica.ViewModels;

namespace BaseDatosMusica.Models;

public partial class GrupoDContext : DbContext
{
    public GrupoDContext()
    {
    }

    public GrupoDContext(DbContextOptions<GrupoDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artista> Artistas { get; set; }

    public virtual DbSet<Cancione> Canciones { get; set; }

    public virtual DbSet<Colaboracione> Colaboraciones { get; set; }

    public virtual DbSet<Concierto> Conciertos { get; set; }

    public virtual DbSet<Disco> Discos { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Gira> Giras { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }
    public IEnumerable Role { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoD;user=as;password=P0t@t0P0t@t0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artista>(entity =>
        {
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha Nacimiento");
            entity.Property(e => e.NombreArtistico)
                .HasMaxLength(50)
                .HasColumnName("Nombre Artistico");
            entity.Property(e => e.NombreReal)
                .HasMaxLength(50)
                .HasColumnName("Nombre Real");

            entity.HasOne(d => d.RolPrincipalNavigation).WithMany(p => p.Artista)
                .HasForeignKey(d => d.RolPrincipal)
                .HasConstraintName("FK_Artistas_Roles");
        });

        modelBuilder.Entity<Cancione>(entity =>
        {
            entity.Property(e => e.DiscosId).HasColumnName("DiscosID");
            entity.Property(e => e.Titulo).HasMaxLength(50);

            entity.HasOne(d => d.Discos).WithMany(p => p.Canciones)
                .HasForeignKey(d => d.DiscosId)
                .HasConstraintName("FK_Canciones_Discos");
        });

        modelBuilder.Entity<Colaboracione>(entity =>
        {
            entity.Property(e => e.ArtistasId).HasColumnName("ArtistasID");
            entity.Property(e => e.GruposId).HasColumnName("GruposID");

            entity.HasOne(d => d.Artistas).WithMany(p => p.Colaboraciones)
                .HasForeignKey(d => d.ArtistasId)
                .HasConstraintName("FK_Colaboraciones_Artistas");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Colaboraciones)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Colaboraciones_Grupos");
        });

        modelBuilder.Entity<Concierto>(entity =>
        {
            entity.Property(e => e.Ciudad).HasMaxLength(50);
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.GirasId).HasColumnName("GirasID");
            entity.Property(e => e.GruposId).HasColumnName("GruposID");
            entity.Property(e => e.Precio).HasColumnType("money");

            object value = entity.HasOne(d => d.Giras).WithMany(p => p.Conciertos as IEnumerable<Concierto>)
                .HasForeignKey(d => d.GirasId)
                .HasConstraintName("FK_Conciertos_Giras");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Conciertos)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Conciertos_Grupos");
        });

        modelBuilder.Entity<Disco>(entity =>
        {
            entity.Property(e => e.GeneroId).HasColumnName("GeneroID");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.Genero).WithMany(p => p.Discos)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK_Discos_Generos");

            entity.HasOne(d => d.Grupos).WithMany(p => p.Discos)
                .HasForeignKey(d => d.GruposId)
                .HasConstraintName("FK_Discos_Grupos");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gira>(entity =>
        {
            entity.Property(e => e.FechaFinal).HasColumnName("Fecha Final");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha Inicio");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.Property(e => e.ManagersId).HasColumnName("ManagersID");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.Managers).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.ManagersId)
                .HasConstraintName("FK_Grupos_Managers");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha Nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<BaseDatosMusica.ViewModels.DiscosPorGrupoViewModel> DiscosPorGrupoViewModel { get; set; } = default!;
}
