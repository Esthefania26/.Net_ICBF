using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProIcbf.Models;
using ProIcbf.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProIcbf.Controllers
{
    public class AsistenciaController : Controller
    {
        private readonly CopyIcbfContext _DBContext;

        public AsistenciaController(CopyIcbfContext context)
        {
            _DBContext = context;
        }

        public IActionResult Asistencia()
        {
            List<Asistencia> lista = _DBContext.Asistencias
                .Include(e => e.oUsuario)
                .Include(e => e.oNino)
                .ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Asistencia_Detalle(int IdAsistencia)
        {
            AsistenciaVM oAsistenciaVM = new AsistenciaVM()
            {
                oAsistencia = new Asistencia(),
                oListaUsuario = _DBContext.Usuarios.Select(usuario => new SelectListItem()
                {
                    Text = usuario.NombreUsuario,
                    Value = usuario.IdUsuario.ToString()
                }).ToList(),
                oListaNino = _DBContext.Ninos.Select(nino => new SelectListItem()
                {
                    Text = nino.Nombre,
                    Value = nino.IdNino.ToString()
                }).ToList()
            };

            if (IdAsistencia != 0)
            {
                oAsistenciaVM.oAsistencia = _DBContext.Asistencias.Find(IdAsistencia);
            }

            return View(oAsistenciaVM);
        }

        [HttpPost]
        public IActionResult Asistencia_Detalle(AsistenciaVM oAsistenciaVM)
        {
            if (oAsistenciaVM.oAsistencia.IdAsistencia == 0)
            {
                _DBContext.Asistencias.Add(oAsistenciaVM.oAsistencia);
            }
            else
            {
                _DBContext.Asistencias.Update(oAsistenciaVM.oAsistencia);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("Asistencia", "Asistencia");
        }

        [HttpGet]
        public IActionResult Eliminar(int IdAsistencia)
        {
            Asistencia oAsistencia = _DBContext.Asistencias.FirstOrDefault(e => e.IdAsistencia == IdAsistencia);

            if (oAsistencia != null)
            {
                _DBContext.Asistencias.Remove(oAsistencia);
                _DBContext.SaveChanges();
            }

            return RedirectToAction("Asistencia", "Asistencia");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            var lista = _DBContext.Asistencias
                .Include(e => e.oUsuario)
                .Include(e => e.oNino)
                .ToList();

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(document, stream).CloseStream = false;
                document.Open();

                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;

                table.AddCell("ID Asistencia");
                table.AddCell("Usuario");
                table.AddCell("Fecha Inasistencia");
                table.AddCell("Estado del Niño");
                table.AddCell("Niño");

                foreach (var asistencia in lista)
                {
                    table.AddCell(asistencia.IdAsistencia.ToString());
                    table.AddCell(asistencia.oUsuario.NombreUsuario);
                    table.AddCell(asistencia.Fecha.HasValue ? asistencia.Fecha.Value.ToString("dd/MM/yyyy") : "Fecha no disponible");
                    table.AddCell(asistencia.Detalles);
                    table.AddCell(asistencia.oNino.Nombre);
                }

                document.Add(table);
                document.Close();

                byte[] byteArray = stream.ToArray();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Position = 0;

                return File(byteArray, "application/pdf", "Asistencias.pdf");
            }
        }
    }
}
