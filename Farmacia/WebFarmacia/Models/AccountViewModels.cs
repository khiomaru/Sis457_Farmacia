using System.ComponentModel.DataAnnotations;

namespace WebFarmacia.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public required string Usuario { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public required string Clave { get; set; }
        [Display(Name = "Recordarme")]
        public bool Recordarme { get; set; }
    }
}
