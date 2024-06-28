using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProIcbf.Models;
using ProIcbf.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ProIcbf.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly CopyIcbfContext _DBContext;

        public UsuarioController(CopyIcbfContext context)
        {
            _DBContext = context;
        }

        public IActionResult Usuario()
        {
            List<Usuario> lista = _DBContext.Usuarios.Include(e => e.oRol).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Usuario_Detalle(int idUsuario)
        {
            UsuarioVM oUsuarioVM = new UsuarioVM()
            {
                oUsuario = new Usuario(),
                oListaRole = _DBContext.Roles.Select(role => new SelectListItem()
                {
                    Text = role.NombreRol,
                    Value = role.IdRol.ToString()
                }).ToList()
            };

            if (idUsuario != 0)
            {
                oUsuarioVM.oUsuario = _DBContext.Usuarios.Find(idUsuario);
            }
            return View(oUsuarioVM);
        }

        [HttpPost]
        public IActionResult Usuario_Detalle(UsuarioVM oUsuarioVM)
        {
            var correoExistente = _DBContext.Usuarios.Any(u => u.Correo == oUsuarioVM.oUsuario.Correo && u.IdUsuario != oUsuarioVM.oUsuario.IdUsuario);
            var cedulaExistente = _DBContext.Usuarios.Any(u => u.Cedula == oUsuarioVM.oUsuario.Cedula && u.IdUsuario != oUsuarioVM.oUsuario.IdUsuario);

            if (correoExistente || cedulaExistente)
            {
                ModelState.AddModelError("", "El correo o la cédula ya están registrados");
                oUsuarioVM.oListaRole = _DBContext.Roles.Select(role => new SelectListItem()
                {
                    Text = role.NombreRol,
                    Value = role.IdRol.ToString()
                }).ToList();

                return RedirectToAction("Usuario", "Usuario");
            }

            if (oUsuarioVM.oUsuario.IdUsuario == 0)
            {
                _DBContext.Usuarios.Add(oUsuarioVM.oUsuario);
            }
            else
            {
                _DBContext.Entry(oUsuarioVM.oUsuario).State = EntityState.Modified;
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Usuario", "Usuario");
        }

        [HttpGet]
        public IActionResult Eliminar(int idUsuario)
        {
            Usuario oUsuario = _DBContext.Usuarios.FirstOrDefault(e => e.IdUsuario == idUsuario);

            if (oUsuario != null)
            {
                _DBContext.Usuarios.Remove(oUsuario);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("Usuario", "Usuario");
        }

        [HttpGet]
        public JsonResult VerificarCorreo(string correo)
        {
            var existe = _DBContext.Usuarios.Any(u => u.Correo == correo);
            return Json(new { existe });
        }

        [HttpGet]
        public JsonResult VerificarCedula(string cedula)
        {
            var existe = _DBContext.Usuarios.Any(u => u.Cedula == cedula);
            return Json(new { existe });
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            var lista = _DBContext.Usuarios.Include(e => e.oRol).ToList();
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(document, stream).CloseStream = false;
                document.Open();

                PdfPTable table = new PdfPTable(10);
                table.WidthPercentage = 100;

                table.AddCell("ID Usuario");
                table.AddCell("Nombre");
                table.AddCell("Correo");
                table.AddCell("Contraseña");
                table.AddCell("Rol");
                table.AddCell("Cedula");
                table.AddCell("Telefono");
                table.AddCell("Celular");
                table.AddCell("Direccion");
                table.AddCell("Fecha Nacimiento");

                foreach (var usuario in lista)
                {
                    table.AddCell(usuario.IdUsuario.ToString());
                    table.AddCell(usuario.NombreUsuario);
                    table.AddCell(usuario.Correo);
                    table.AddCell(usuario.Contrasena);
                    table.AddCell(usuario.oRol.NombreRol);
                    table.AddCell(usuario.Cedula);
                    table.AddCell(usuario.Telefono);
                    table.AddCell(usuario.Celular);
                    table.AddCell(usuario.Direccion);
                    table.AddCell(usuario.FechaNacimiento.HasValue ? usuario.FechaNacimiento.Value.ToString("d") : string.Empty);
                }

                document.Add(table);
                document.Close();

                byte[] byteArray = stream.ToArray();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Position = 0;

                return File(byteArray, "application/pdf", "Usuarios.pdf");
            }
        }
    }
}