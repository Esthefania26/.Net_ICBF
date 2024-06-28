using System;
using System.Collections.Generic;

namespace ProIcbf.Models;

public partial class Nino
{
    public int IdNino { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Sangre { get; set; }

    public string? Ciudad { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Eps { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdJardin { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    public virtual Jardine? oJardin { get; set; }

    public virtual Usuario? oUsuario { get; set; }

    public virtual ICollection<ReporteAcademico> ReportesAcademicos { get; set; } = new List<ReporteAcademico>();
}
