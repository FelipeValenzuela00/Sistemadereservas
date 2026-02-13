using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = ErrorMessages.Requerido)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMessages.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.Requerido)]
        [Display (Name = "Rol del Usuario")]
        public int RolId { get; set; }


        [Required(ErrorMessage = ErrorMessages.Requerido)]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailInvalido)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }


    }
}
