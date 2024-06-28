using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProIcbf.Models.ViewModels
{
    public class UsuarioVM
    {

        public Usuario oUsuario {  get; set; }
        public List<SelectListItem> oListaRole {  get; set; }


    }
}
