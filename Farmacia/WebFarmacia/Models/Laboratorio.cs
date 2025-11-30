using System;
using System.Collections.Generic;

namespace WebFarmacia.Models;

public partial class Laboratorio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Pais { get; set; }

    public short Estado { get; set; } = 1;

    public virtual ICollection<Medicamento> Medicamentos { get; set; } = [];
}