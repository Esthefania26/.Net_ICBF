using System;
using System.Collections.Generic;

namespace ProIcbf.Models;

public partial class Jardine
{
    public int IdJardin { get; set; }

    public string? NombreJardin { get; set; }

    public string? Direccion { get; set; }

    public string? Estado { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? oUsuario { get; set; }

    public virtual ICollection<Nino> Ninos { get; set; } = new List<Nino>();
}
