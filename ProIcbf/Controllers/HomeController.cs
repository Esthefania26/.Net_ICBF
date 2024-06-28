
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProIcbf.Models;
using ProIcbf.Models.ViewModels;
using System.Diagnostics;

namespace ProIcbf.Controllers
{
    public class HomeController : Controller
    {
        private readonly CopyIcbfContext _DBcontext;

        public HomeController(CopyIcbfContext context)
        {
            _DBcontext = context;
        }

        public IActionResult Index()
        {
            List<Role> lista = _DBcontext.Roles.OrderBy(r => r.IdRol).ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Roles_Detalle(int idRol)
        {
            RolVM oRolVM = new RolVM()
            {
                oRol = new Role(),
            };
            if (idRol != 0)
            {
                oRolVM.oRol = _DBcontext.Roles.Find(idRol);
            }

            return View(oRolVM);
        }
        [HttpPost]
        public IActionResult Roles_Detalle(RolVM oRolVM)
        {
            // Verificar si ya existe un rol con el mismo nombre
            bool rolExists = _DBcontext.Roles.Any(r => r.NombreRol == oRolVM.oRol.NombreRol && r.IdRol != oRolVM.oRol.IdRol);

            if (rolExists)
            {
               
                ModelState.AddModelError("oRol.NombreRol", "Ya existe un rol con este nombre.");
               
                return View(oRolVM);
            }

            if (oRolVM.oRol.IdRol == 0)
            {
                _DBcontext.Roles.Add(oRolVM.oRol);
            }
            else
            {
                _DBcontext.Roles.Update(oRolVM.oRol);
            }
            _DBcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Eliminar(int idRol)
        {
            Role oRol = _DBcontext.Roles.FirstOrDefault(e => e.IdRol == idRol);
            if (oRol != null)
            {
                _DBcontext.Roles.Remove(oRol);
                _DBcontext.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
