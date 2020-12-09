using System.ComponentModel.DataAnnotations;

namespace ProjectMann.Web.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email tiene un formato invalido")]
        public string Email { get; set; }
        
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }

        public bool IsBadLogin { get; set; } = false;
    }
}