using Microsoft.AspNetCore.Mvc.Rendering;
using ProIcbf.Models; // Asegúrate de incluir el espacio de nombres correcto para Nino

namespace ProIcbf.Models.ViewModels
{
    public class NinoVM
    {
        public Nino oNino { get; set; }

        public List<SelectListItem> oListaUsuario { get; set; }
        public List<SelectListItem> oListaJardin { get; set; }
    }
}
