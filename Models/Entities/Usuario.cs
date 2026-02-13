
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = ErrorMessages.Requerido)]
    [StringLength (20, MinimumLength =3, ErrorMessage = ErrorMessages.RangoCaracteres)]
    [RegularExpression("^[a-z0-9]+$", ErrorMessage = ErrorMessages.UserNameIncorrecto)]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    public string PasswordHash { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    [Display(Name = "Funcion")]
    public int RolId { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    [Display(Name = "Funcion")]
    public virtual Role Rol { get; set; } = null!;

    public virtual ICollection<UsuarioEmail> UsuarioEmails { get; set; } = new List<UsuarioEmail>();

    [NotMapped]
    [Display(Name = "Email")]
    public string? EmailPrincipal => UsuarioEmails.FirstOrDefault(e => e.EsPrincipal == true)?.Email;
}
