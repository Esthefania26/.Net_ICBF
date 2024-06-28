using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProIcbf.Models;
using ProIcbf.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace ProIcbf.Controllers
{
    public class NinoController : Controller
    {
        private readonly CopyIcbfContext _DBContext;

        public NinoController(CopyIcbfContext context)
        {
            _DBContext = context;
        }

        public IActionResult Nino()
        {
            List<Nino> lista = _DBContext.Ninos
                .Include(e => e.oUsuario)
                .Include(e => e.oJardin)
                .ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Nino_Detalle(int IdNino)
        {
            NinoVM oNinoVM = new NinoVM()
            {
                oNino = new Nino(),
                oListaUsuario = _DBContext.Usuarios.Select(usuario => new SelectListItem()
                {
                    Text = usuario.Cedula,
                    Value = usuario.IdUsuario.ToString()
                }).ToList(),
                oListaJardin = _DBContext.Jardines.Select(jardin => new SelectListItem()
                {
                    Text = jardin.NombreJardin,
                    Value = jardin.IdJardin.ToString()
                }).ToList()
            };

            if (IdNino != 0)
            {
                oNinoVM.oNino = _DBContext.Ninos.Find(IdNino);
            }

            return View(oNinoVM);
        }

        [HttpPost]
        public IActionResult Nino_Detalle(NinoVM oNinoVM)
        {
            // Verificar si el nombre del niño ya existe
            var nombreNinoExistente = _DBContext.Ninos.Any(n => n.Nombre == oNinoVM.oNino.Nombre && n.IdNino != oNinoVM.oNino.IdNino);

            if (nombreNinoExistente)
            {
                // Mostrar mensaje de error
                ModelState.AddModelError("", "Ya existe un niño registrado con este nombre");

                // Cargar listas para los dropdowns
                oNinoVM.oListaUsuario = _DBContext.Usuarios.Select(usuario => new SelectListItem()
                {
                    Text = usuario.Cedula,
                    Value = usuario.IdUsuario.ToString()
                }).ToList();

                oNinoVM.oListaJardin = _DBContext.Jardines.Select(jardin => new SelectListItem()
                {
                    Text = jardin.NombreJardin,
                    Value = jardin.IdJardin.ToString()
                }).ToList();

                return View(oNinoVM); 
            }

            // Si no hay errores de duplicados, procedemos con el guardado del niño
            if (oNinoVM.oNino.IdNino == 0)
            {
                // Nuevo niño, se agrega al contexto
                _DBContext.Ninos.Add(oNinoVM.oNino);
            }
            else
            {
               
                _DBContext.Ninos.Update(oNinoVM.oNino);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Nino", "Nino"); 
        }

        [HttpGet]
        public IActionResult Eliminar(int IdNino)
        {
            Nino oNino = _DBContext.Ninos.FirstOrDefault(e => e.IdNino == IdNino);

            if (oNino != null)
            {
                _DBContext.Ninos.Remove(oNino);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("Nino", "Nino");
        }

        [HttpGet]
        public JsonResult VerificarNombreNino(string nombreNino)
        {
            var existe = _DBContext.Ninos.Any(n => n.Nombre == nombreNino);
            return Json(new { existe });
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            var lista = _DBContext.Ninos.Include(e => e.oUsuario).Include(e => e.oJardin).ToList();
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(document, stream).CloseStream = false;
                document.Open();

                PdfPTable table = new PdfPTable(10);
                table.WidthPercentage = 100;

                table.AddCell("ID Niño");
                table.AddCell("Nombre");
                table.AddCell("Fecha Nacimiento");
                table.AddCell("Tipo Sangre");
                table.AddCell("Ciudad");
                table.AddCell("Teléfono");
                table.AddCell("Dirección");
                table.AddCell("EPS");
                table.AddCell("ID Acudiente");
                table.AddCell("Jardín");

                foreach (var nino in lista)
                {
                    table.AddCell(nino.IdNino.ToString());
                    table.AddCell(nino.Nombre);
                    table.AddCell(nino.Fecha.ToString()); 
                    table.AddCell(nino.Sangre);
                    table.AddCell(nino.Ciudad);
                    table.AddCell(nino.Telefono);
                    table.AddCell(nino.Direccion);
                    table.AddCell(nino.Eps);
                    table.AddCell(nino.oUsuario?.Cedula ?? "N/A");
                    table.AddCell(nino.oJardin?.NombreJardin ?? "N/A");
                }

                document.Add(table);
                document.Close();

                byte[] byteArray = stream.ToArray();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Position = 0;

                return File(byteArray, "application/pdf", "Ninos.pdf");
            }
        }
    }
}
