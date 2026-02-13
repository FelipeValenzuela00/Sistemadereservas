using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entities;

public partial class Role
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
