using System;
using System.Collections.Generic;

namespace ProIcbf.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Contrasena { get; set; }

    public string? Correo { get; set; }

    public int? IdRol { get; set; }

    public string? Nombre { get; set; }

    public string? Cedula { get; set; }

    public string? Telefono { get; set; }

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Role? oRol { get; set; }

    public virtual ICollection<Jardine> Jardines { get; set; } = new List<Jardine>();

    public virtual ICollection<Nino> Ninos { get; set; } = new List<Nino>();

    public virtual ICollection<ReporteAcademico> ReportesAcademicos { get; set; } = new List<ReporteAcademico>();
}
