using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFarmacia.Models
{
    public partial class Reserva
    {
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El medicamento es obligatorio")]
        public int IdMedicamento { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio")]
        [StringLength(200, ErrorMessage = "El motivo no puede exceder los 200 caracteres")]
        public string Motivo { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Las notas no pueden exceder los 500 caracteres")]
        public string? Notas { get; set; }

        [Required(ErrorMessage = "La fecha de reserva es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Reserva")]
        public DateTime FechaReserva { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento de la reserva es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Vencimiento Reserva")]
        public DateTime FechaVencimientoReserva { get; set; }

        [Required(ErrorMessage = "El teléfono de contacto es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres")]
        [Display(Name = "Teléfono Contacto")]
        public string TelefonoContacto { get; set; } = null!;

        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [Display(Name = "Email Contacto")]
        public string? EmailContacto { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "PENDIENTE"; // PENDIENTE, CONFIRMADA, CANCELADA, EXPIRADA

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;

        public virtual Medicamento IdMedicamentoNavigation { get; set; } = null!;
    }
}