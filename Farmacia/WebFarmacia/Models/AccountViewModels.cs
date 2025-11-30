using System.ComponentModel.DataAnnotations;

namespace WebFarmacia.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [Display(Name = "Recordarme")]
        public bool Recordarme { get; set; }
    }
}
