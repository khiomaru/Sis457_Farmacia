using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebFarmacia.Models;

public partial class Categoria
{
        public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
    public string Nombre { get; set; } = null!;

    [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres")]
    public string? Descripcion { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<Medicamento> Medicamentos { get; set; } = [];
}
