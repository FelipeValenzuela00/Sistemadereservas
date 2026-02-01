using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Reserva
{
    public int ReservasId { get; set; }

    public int UsuarioId { get; set; }

    public int RecursoId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string? Estado { get; set; }

    public virtual Recurso Recurso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
