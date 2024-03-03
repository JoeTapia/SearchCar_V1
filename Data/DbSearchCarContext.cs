using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SearchCar_V1.Data;

public partial class DbSearchCarContext : DbContext
{
    public DbSearchCarContext()
    {
    }

    public DbSearchCarContext(DbContextOptions<DbSearchCarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAsignacion> TblAsignacions { get; set; }

    public virtual DbSet<TblCarro> TblCarros { get; set; }

    public virtual DbSet<TblCiudad> TblCiudads { get; set; }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblLocalidad> TblLocalidads { get; set; }

    public virtual DbSet<TblMarca> TblMarcas { get; set; }

    public virtual DbSet<TblPai> TblPais { get; set; }

    public virtual DbSet<TblSede> TblSedes { get; set; }

    public virtual DbSet<TblTipoCarro> TblTipoCarros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Database=db_SearchCar;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAsignacion>(entity =>
        {
            entity.HasKey(e => e.IdAsignacion).HasName("PK_Tbl_Asigancion");

            entity.ToTable("Tbl_Asignacion");

            entity.Property(e => e.IdAsignacion).HasColumnName("id_Asignacion");
            entity.Property(e => e.Cant).HasColumnName("cant");
            entity.Property(e => e.IdCarro).HasColumnName("id_Carro");
            entity.Property(e => e.IdSede).HasColumnName("id_Sede");
        });

        modelBuilder.Entity<TblCarro>(entity =>
        {
            entity.HasKey(e => e.IdCarro);

            entity.ToTable("Tbl_Carro");

            entity.Property(e => e.IdCarro).HasColumnName("id_Carro");
            entity.Property(e => e.CodigoCarro)
                .HasMaxLength(50)
                .HasColumnName("codigo_Carro");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.IdMarca).HasColumnName("id_Marca");
            entity.Property(e => e.IdTipoCarro).HasColumnName("id_TipoCarro");
            entity.Property(e => e.Modelo)
                .HasMaxLength(150)
                .HasColumnName("modelo");
            entity.Property(e => e.PotenciaMotor)
                .HasMaxLength(50)
                .HasColumnName("potencia_motor");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");
            entity.Property(e => e.Version)
                .HasMaxLength(150)
                .HasColumnName("version");
        });

        modelBuilder.Entity<TblCiudad>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK_Tbl_Pais");

            entity.ToTable("Tbl_Ciudad");

            entity.Property(e => e.IdCiudad).HasColumnName("id_Ciudad");
            entity.Property(e => e.IdPais).HasColumnName("id_Pais");
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("nombre_Ciudad");
        });

        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Tbl_Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
            entity.Property(e => e.ApellidoCliente)
                .HasMaxLength(250)
                .HasColumnName("apellido_Cliente");
            entity.Property(e => e.DireccionCliente)
                .HasMaxLength(150)
                .HasColumnName("direccion_Cliente");
            entity.Property(e => e.DocIdentidadCliente)
                .HasMaxLength(20)
                .HasColumnName("docIdentidad_Cliente");
            entity.Property(e => e.IdLocalidad).HasColumnName("id_Localidad");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(250)
                .HasColumnName("nombre_Cliente");
            entity.Property(e => e.TelefonoCliente)
                .HasMaxLength(20)
                .HasColumnName("telefono_Cliente");
        });

        modelBuilder.Entity<TblLocalidad>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad);

            entity.ToTable("Tbl_Localidad");

            entity.Property(e => e.IdLocalidad).HasColumnName("id_Localidad");
            entity.Property(e => e.IdCiudad).HasColumnName("id_Ciudad");
            entity.Property(e => e.NombreLocalidad)
                .HasMaxLength(150)
                .HasColumnName("nombre_Localidad");
        });

        modelBuilder.Entity<TblMarca>(entity =>
        {
            entity.HasKey(e => e.IdMarca);

            entity.ToTable("Tbl_Marca");

            entity.Property(e => e.IdMarca).HasColumnName("id_Marca");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(150)
                .HasColumnName("nombre_Marca");
        });

        modelBuilder.Entity<TblPai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK_Tbl_Pais_1");

            entity.ToTable("Tbl_Pais");

            entity.Property(e => e.IdPais).HasColumnName("id_Pais");
            entity.Property(e => e.Pais).HasMaxLength(150);
        });

        modelBuilder.Entity<TblSede>(entity =>
        {
            entity.HasKey(e => e.IdSede);

            entity.ToTable("Tbl_Sede");

            entity.Property(e => e.IdSede).HasColumnName("id_Sede");
            entity.Property(e => e.CantMaxima).HasColumnName("cant_Maxima");
            entity.Property(e => e.DireccionSede)
                .HasMaxLength(150)
                .HasColumnName("direccion_Sede");
            entity.Property(e => e.IdLocalidad).HasColumnName("id_Localidad");
            entity.Property(e => e.NombreSede)
                .HasMaxLength(150)
                .HasColumnName("nombre_Sede");
        });

        modelBuilder.Entity<TblTipoCarro>(entity =>
        {
            entity.HasKey(e => e.IdTipoCarro);

            entity.ToTable("Tbl_TipoCarro");

            entity.Property(e => e.IdTipoCarro).HasColumnName("id_TipoCarro");
            entity.Property(e => e.TipoCarro).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
