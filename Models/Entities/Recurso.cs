using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Entities;

public partial class Recurso
{
    public int RecursoId { get; set; }

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    [StringLength(20, ErrorMessage = ErrorMessages.MaximosCaracteres)]
    public string Tipo { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    [Range(1,1000, ErrorMessage = ErrorMessages.RangoNumerico)]
    public int? Capacidad { get; set; }

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    [DataType(DataType.MultilineText)]
    [StringLength(200, ErrorMessage = ErrorMessages.MaximosCaracteres)]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = ErrorMessages.Requerido)]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
