namespace ProIcbf.Models.ViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ProIcbf.Models;

    public class ReporteAcademicoVM
    {
        public ReporteAcademico oReporteAcademico { get; set; }
        public List<SelectListItem> oListaUsuario { get; set; }
        public List<SelectListItem> oListaNino { get; set; }
    }
}
