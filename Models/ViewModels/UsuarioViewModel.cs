using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Display (Name = "Rol del Usuario")]
        public int RolId { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }


    }
}
