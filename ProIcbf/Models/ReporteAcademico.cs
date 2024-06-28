using System;
using System.Collections.Generic;

namespace ProIcbf.Models;

public partial class ReporteAcademico
{
    public int IdReporte { get; set; }

    public int? AnoEscolar { get; set; }

    public string? Nivel { get; set; }

    public string? Notas { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public int? IdNino { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Nino? oNino { get; set; }

    public virtual Usuario? oUsuario { get; set; }
}
