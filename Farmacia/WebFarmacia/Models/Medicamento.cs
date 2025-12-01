using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebFarmacia.Models;

public partial class Medicamento
{
        public int Id { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public int IdCategoria { get; set; }

    [Required(ErrorMessage = "El laboratorio es obligatorio")]
    public int IdLaboratorio { get; set; }

    [Required(ErrorMessage = "El código es obligatorio")]
    [StringLength(50, ErrorMessage = "El código no puede exceder los 50 caracteres")]
    public string Codigo { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
    public string Nombre { get; set; } = null!;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
    public string? Descripcion { get; set; }

    [StringLength(1000, ErrorMessage = "La composición no puede exceder los 1000 caracteres")]
    public string? Composicion { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Vencimiento")]
    public DateTime FechaVencimiento { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser mayor a 0")]
    public int Stock { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    [DataType(DataType.Currency)]
    [Display(Name = "Precio de Venta")]
    public decimal PrecioVenta { get; set; }

    public bool RequiereReceta { get; set; } = false;

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = [];
}
