using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }
    [Required]
    public string Nombre { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    [Required]
    public int RolId { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    [Required]
    public virtual Role Rol { get; set; } = null!;

    public virtual ICollection<UsuarioEmail> UsuarioEmails { get; set; } = new List<UsuarioEmail>();

    [NotMapped]
    public string? EmailPrincipal => UsuarioEmails.FirstOrDefault(e => e.EsPrincipal == true)?.Email;
}
