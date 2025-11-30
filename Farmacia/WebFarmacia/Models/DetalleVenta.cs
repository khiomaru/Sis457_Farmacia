using System;
using System.Collections.Generic;

namespace WebFarmacia.Models;

public partial class VentaDetalle
{
    public int IdDetalleVenta { get; set; }

    public int IdVenta { get; set; }

    public int IdMedicamento { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubTotal { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
