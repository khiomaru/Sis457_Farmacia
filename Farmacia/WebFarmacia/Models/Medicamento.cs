using System;
using System.Collections.Generic;

namespace WebFarmacia.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    public int IdLaboratorio { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Composicion { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public int Stock { get; set; }

    public decimal PrecioVenta { get; set; }

    public bool RequiereReceta { get; set; } = false;

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
