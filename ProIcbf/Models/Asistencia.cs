using System;
using System.Collections.Generic;

namespace ProIcbf.Models
{
    public partial class Asistencia
    {
        public int IdAsistencia { get; set; }

        public int? IdUsuario { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? Detalles { get; set; }

        public int? IdNino { get; set; }

        public virtual Nino? oNino { get; set; }

        public virtual Usuario? oUsuario { get; set; }
    }
}
