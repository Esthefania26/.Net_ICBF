using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProIcbf.Models;
using ProIcbf.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ClosedXML.Excel;
using System.IO;

namespace ProIcbf.Controllers
{
    public class JardinController : Controller
    {
        private readonly CopyIcbfContext _DBContext;

        public JardinController(CopyIcbfContext context)
        {
            _DBContext = context;
        }

        public IActionResult Jardin()
        {
            List<Jardine> lista = _DBContext.Jardines.Include(e => e.oUsuario).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Jardin_Detalle(int idJardin)
        {
            JardinVM oJardinVM = new JardinVM()
            {
                oJardin = new Jardine(),
                oListaUsuario = _DBContext.Usuarios.Select(Usuario => new SelectListItem()
                {
                    Text = Usuario.NombreUsuario,
                    Value = Usuario.IdUsuario.ToString()
                }).ToList()
            };

            if (idJardin != 0)
            {
                oJardinVM.oJardin = _DBContext.Jardines.Find(idJardin);
            }

            return View(oJardinVM);
        }

        [HttpPost]
        public IActionResult Jardin_Detalle(JardinVM oJardinVM)
        {
            var nombreJardinExistente = _DBContext.Jardines.Any(j => j.NombreJardin == oJardinVM.oJardin.NombreJardin && j.IdJardin != oJardinVM.oJardin.IdJardin);

            if (nombreJardinExistente)
            {
                ModelState.AddModelError("", "El nombre del jardín ya está registrado");
                oJardinVM.oListaUsuario = _DBContext.Usuarios.Select(usuario => new SelectListItem()
                {
                    Text = usuario.NombreUsuario,
                    Value = usuario.IdUsuario.ToString()
                }).ToList();

                return RedirectToAction("Jardin", "Jardin");
            }

            if (oJardinVM.oJardin.IdJardin == 0)
            {
                _DBContext.Jardines.Add(oJardinVM.oJardin);
            }
            else
            {
                _DBContext.Jardines.Update(oJardinVM.oJardin);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Jardin", "Jardin");
        }

        [HttpGet]
        public IActionResult Eliminar(int idJardin)
        {
            Jardine oJardin = _DBContext.Jardines.FirstOrDefault(e => e.IdJardin == idJardin);

            if (oJardin != null)
            {
                _DBContext.Jardines.Remove(oJardin);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("Jardin", "Jardin");
        }

        [HttpGet]
        public JsonResult VerificarNombreJardin(string nombreJardin)
        {
            var existe = _DBContext.Jardines.Any(j => j.NombreJardin == nombreJardin);
            return Json(new { existe });
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var lista = _DBContext.Jardines.Include(e => e.oUsuario).ToList();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Jardines");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Nombre";
                worksheet.Cell(currentRow, 3).Value = "Direccion";
                worksheet.Cell(currentRow, 4).Value = "Estado";
                worksheet.Cell(currentRow, 5).Value = "Madre Comunitaria";

                foreach (var jardin in lista)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = jardin.IdJardin;
                    worksheet.Cell(currentRow, 2).Value = jardin.NombreJardin;
                    worksheet.Cell(currentRow, 3).Value = jardin.Direccion;
                    worksheet.Cell(currentRow, 4).Value = jardin.Estado;
                    worksheet.Cell(currentRow, 5).Value = jardin.oUsuario?.NombreUsuario ?? "N/A";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Jardines.xlsx");
                }
            }
        }
    }
}