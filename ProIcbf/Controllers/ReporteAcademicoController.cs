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
    public class ReporteAcademicoController : Controller
    {
        private readonly CopyIcbfContext _DBContext;

        public ReporteAcademicoController(CopyIcbfContext context)
        {
            _DBContext = context;
        }

        public IActionResult ReporteAcademico()
        {
            List<ReporteAcademico> lista = _DBContext.ReportesAcademicos
                .Include(e => e.oUsuario)
                .Include(e => e.oNino)
                .ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult ReporteAcademico_Detalle(int IdReporte)
        {
            ReporteAcademicoVM oReporteAcademicoVM = new ReporteAcademicoVM()
            {
                oReporteAcademico = new ReporteAcademico(),
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

            if (IdReporte != 0)
            {
                oReporteAcademicoVM.oReporteAcademico = _DBContext.ReportesAcademicos.Find(IdReporte);
            }

            return View(oReporteAcademicoVM);
        }

        [HttpPost]
        public IActionResult ReporteAcademico_Detalle(ReporteAcademicoVM oReporteAcademicoVM)
        {
            if (oReporteAcademicoVM.oReporteAcademico.IdReporte == 0)
            {
                _DBContext.ReportesAcademicos.Add(oReporteAcademicoVM.oReporteAcademico);
            }
            else
            {
                _DBContext.ReportesAcademicos.Update(oReporteAcademicoVM.oReporteAcademico);
            }
            _DBContext.SaveChanges();
            return RedirectToAction("ReporteAcademico", "ReporteAcademico");
        }

        [HttpGet]
        public IActionResult Eliminar(int IdReporte)
        {
            ReporteAcademico oReporteAcademico = _DBContext.ReportesAcademicos.FirstOrDefault(e => e.IdReporte == IdReporte);

            if (oReporteAcademico != null)
            {
                _DBContext.ReportesAcademicos.Remove(oReporteAcademico);
                _DBContext.SaveChanges();
            }

            return RedirectToAction("ReporteAcademico", "ReporteAcademico");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            var lista = _DBContext.ReportesAcademicos
                .Include(e => e.oUsuario)
                .Include(e => e.oNino)
                .ToList();

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(document, stream).CloseStream = false;
                document.Open();

                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100;

                table.AddCell("ID Reporte");
                table.AddCell("Año Escolar");
                table.AddCell("Nivel");
                table.AddCell("Notas");
                table.AddCell("Descripción");
                table.AddCell("Fecha Entrega");
                table.AddCell("Niño");
                table.AddCell("Usuario");

                foreach (var reporte in lista)
                {
                    table.AddCell(reporte.IdReporte.ToString());
                    table.AddCell(reporte.AnoEscolar.ToString());
                    table.AddCell(reporte.Nivel);
                    table.AddCell(reporte.Notas);
                    table.AddCell(reporte.Descripcion);
                    table.AddCell(reporte.FechaEntrega.HasValue ? reporte.FechaEntrega.Value.ToString("dd/MM/yyyy") : "Fecha no disponible");
                    table.AddCell(reporte.oNino?.Nombre);
                    table.AddCell(reporte.oUsuario?.NombreUsuario);
                }

                document.Add(table);
                document.Close();

                byte[] byteArray = stream.ToArray();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Position = 0;

                return File(byteArray, "application/pdf", "ReportesAcademicos.pdf");
            }
        }
    }
}
