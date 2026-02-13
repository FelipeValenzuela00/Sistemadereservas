using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entities;

public partial class UsuarioEmail
{
    public int EmailId { get; set; }

    public int UsuarioId { get; set; }

    public string Email { get; set; } = null!;

    public bool? EsPrincipal { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
