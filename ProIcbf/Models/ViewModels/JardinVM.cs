using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProIcbf.Models.ViewModels
{
    public class JardinVM
    {

        public Jardine oJardin { get; set; }

        public List<SelectListItem> oListaUsuario {  get; set; }    


    }
}
