using System;
using System.Collections.Generic;

namespace WebFarmacia.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string CedulaIdentidad { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public long? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();

    public virtual ICollection<Venta> Ventas { get; set; } = [];
}
