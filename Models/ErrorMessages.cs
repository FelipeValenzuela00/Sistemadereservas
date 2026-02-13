namespace WebApplication1.Models
{
    public class ErrorMessages
    {
        public const string Requerido = "El campo {0} es requerido. ";
        public const string RangoCaracteres = "El campo {0} debe tener entre {2} y {1} caracteres. ";
        public const string LongitudNumerica = "El campo {0} debe tener entre numeros en la cantidad adecuada de digitos.";
        public const string EmailInvalido = "El campo {0} debe ser un correo electrónico válido. ";
        public const string MaximosCaracteres = "El campo {0} no puede tener más de {1} caracteres. ";
        public const string RangoNumerico = "El campo {0} debe tener un valor entre {1} y {2}.";
        public const string ValorDebeSerUnoDe = "El campo {0} debe ser {1} o {2}.";
        public const string LongitudExacta = "El campo {0} debe contener exactamente {1} carácter(es).";
        public const string SoloMayuscula = "El campo {0} debe ser una única letra mayúscula (A-Z).";
        public const string PasswordConfirmado = "La contraseña y su confirmación no coinciden.";
        public const string UserNameIncorrecto = "Solo se permiten letras minúsculas y números, sin espacios.";
    }
}
