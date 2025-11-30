using System;
using Microsoft.EntityFrameworkCore;

namespace WebFarmacia.Models
{
    public class FarmaciaContext : DbContext
    {
        public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options)
        {
        }

        public virtual DbSet<Categorium> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Laboratorio> Laboratorios { get; set; }
        public virtual DbSet<Producto> Medicamentos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);
                
                entity.Property(e => e.IdProducto).ValueGeneratedOnAdd();
                entity.Property(e => e.Codigo).HasMaxLength(50);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Composicion).HasMaxLength(1000);
                
                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Producto_Categoria");
                    
                entity.HasOne(d => d.IdLaboratorioNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdLaboratorio)
                    .HasConstraintName("FK_Producto_Laboratorio");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta);
                
                entity.Property(e => e.IdVenta).ValueGeneratedOnAdd();
                entity.Property(e => e.NumeroFactura).HasMaxLength(50);
                entity.Property(e => e.FechaVenta).HasColumnType("datetime");
                
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Venta_Usuario");
                    
                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Venta_Cliente");
            });

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta);
                
                entity.Property(e => e.IdDetalleVenta).ValueGeneratedOnAdd();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SubTotal).HasColumnType("decimal(18,2)");
                
                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK_VentaDetalle_Venta");
                    
                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.IdMedicamento)
                    .HasConstraintName("FK_VentaDetalle_Producto");
            });

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);
                entity.Property(e => e.IdCategoria).ValueGeneratedOnAdd();
                entity.Property(e => e.Descripcion).HasMaxLength(100);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CedulaIdentidad).HasMaxLength(20);
                entity.Property(e => e.Nombres).HasMaxLength(100);
                entity.Property(e => e.Apellidos).HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(20);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("idEmpleado").ValueGeneratedOnAdd();
                entity.Property(e => e.CedulaIdentidad).HasMaxLength(20);
                entity.Property(e => e.Nombres).HasMaxLength(100);
                entity.Property(e => e.PrimerApellido).HasMaxLength(100);
                entity.Property(e => e.SegundoApellido).HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.Celular);
                entity.Property(e => e.Cargo).HasMaxLength(50);
                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            });

            modelBuilder.Entity<Laboratorio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Pais).HasMaxLength(100);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.IdUsuario).ValueGeneratedOnAdd();
                entity.Property(e => e.Usuario1).HasMaxLength(50);
                entity.Property(e => e.Clave).HasMaxLength(100);
                entity.Property(e => e.UsuarioRegistro).HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            });
        }

    }
}