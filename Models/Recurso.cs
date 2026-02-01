using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Recurso
{
    public int RecursoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int? Capacidad { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
