using System;
using Microsoft.EntityFrameworkCore;

namespace WebFarmacia.Models
{
    public class FarmaciaContext : DbContext
    {
        public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Laboratorio> Laboratorios { get; set; }
        public virtual DbSet<Medicamento> Medicamentos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.ToTable("Medicamento");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
                entity.Property(e => e.IdLaboratorio).HasColumnName("idLaboratorio");
                entity.Property(e => e.Codigo).HasColumnName("codigo").HasMaxLength(20);
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(250);
                entity.Property(e => e.Composicion).HasColumnName("composicion").HasMaxLength(250);
                entity.Property(e => e.FechaVencimiento).HasColumnName("fechaVencimiento").HasColumnType("date");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta").HasColumnType("decimal(10,2)");
                entity.Property(e => e.RequiereReceta).HasColumnName("requiereReceta");
                entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
                entity.Property(e => e.Estado).HasColumnName("estado");
                
                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Medicamento_Categoria");
                    
                entity.HasOne(d => d.IdLaboratorioNavigation)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.IdLaboratorio)
                    .HasConstraintName("FK_Medicamento_Laboratorio");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Venta");
                entity.HasKey(e => e.IdVenta);
                
                entity.Property(e => e.IdVenta).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
                entity.Property(e => e.IdCliente).HasColumnName("idCliente");
                entity.Property(e => e.NumeroFactura).HasColumnName("numeroFactura").HasMaxLength(50).HasComputedColumnSql("('FAC-'+CONVERT([varchar](10),[id]))", stored: false);
                entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(10,2)");
                entity.Property(e => e.FechaVenta).HasColumnName("fechaVenta").HasColumnType("datetime");
                entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
                entity.Property(e => e.Estado).HasColumnName("estado");
                
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("fk_Venta_Usuario");
                    
                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("fk_Venta_Cliente");
            });

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.ToTable("DetalleVenta");
                entity.HasKey(e => e.IdDetalleVenta);
                
                entity.Property(e => e.IdDetalleVenta).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.IdVenta).HasColumnName("idVenta");
                entity.Property(e => e.IdMedicamento).HasColumnName("idMedicamento");
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
                entity.Property(e => e.PrecioUnitario).HasColumnName("precioUnitario").HasColumnType("decimal(10,2)");
                entity.Property(e => e.SubTotal).HasColumnName("subtotal").HasColumnType("decimal(10,2)").HasComputedColumnSql("([cantidad]*[precioUnitario])", stored: false);
                entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
                entity.Property(e => e.Estado).HasColumnName("estado");
                
                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("fk_DetalleVenta_Venta");
                    
                entity.HasOne(d => d.IdMedicamentoNavigation)
                    .WithMany(p => p.VentaDetalles)
                    .HasForeignKey(d => d.IdMedicamento)
                    .HasConstraintName("fk_DetalleVenta_Medicamento");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categoria");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(250);
                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.CedulaIdentidad).HasColumnName("cedulaIdentidad").HasMaxLength(12);
                entity.Property(e => e.Nombres).HasColumnName("nombres").HasMaxLength(100);
                entity.Property(e => e.Apellidos).HasColumnName("apellidos").HasMaxLength(100);
                entity.Property(e => e.Telefono).HasColumnName("telefono");
                entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(250);
                entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.CedulaIdentidad).HasColumnName("cedulaIdentidad").HasMaxLength(12);
                entity.Property(e => e.Nombres).HasColumnName("nombres").HasMaxLength(50);
                entity.Property(e => e.PrimerApellido).HasColumnName("primerApellido").HasMaxLength(50);
                entity.Property(e => e.SegundoApellido).HasColumnName("segundoApellido").HasMaxLength(50);
                entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(250);
                entity.Property(e => e.Celular).HasColumnName("celular");
                entity.Property(e => e.Cargo).HasColumnName("cargo").HasMaxLength(50);
                entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<Laboratorio>(entity =>
            {
                entity.ToTable("Laboratorio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
                entity.Property(e => e.Pais).HasColumnName("pais").HasMaxLength(50);
                entity.Property(e => e.Estado).HasColumnName("estado");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.IdUsuario).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
                entity.Property(e => e.Usuario1).HasColumnName("usuario").HasMaxLength(50);
                entity.Property(e => e.Clave).HasColumnName("clave").HasMaxLength(255);
                entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro").HasMaxLength(50);
                entity.Property(e => e.FechaRegistro).HasColumnName("fechaRegistro").HasColumnType("datetime");
                entity.Property(e => e.Estado).HasColumnName("estado");
                
                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("fk_Usuario_Empleado");
            });
        }

    }
}