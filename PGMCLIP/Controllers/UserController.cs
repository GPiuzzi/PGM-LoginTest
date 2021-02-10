using PGMCLIP.AccesoDatos;
using PGMCLIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.draw;
using PGMCLIP.Configuracion;
using System.Data.SqlClient;

namespace PGMCLIP.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost] // para enviar el dato

        public ActionResult Login(string usuario, string contraseña)
        {

            bool resultado = UsuarioAD.IniciarSesion(usuario, contraseña);

            if (ModelState.IsValid && usuario != null && contraseña != null && resultado)
            {
                return RedirectToAction("Index", "home");
            }
            else
            {
                return View();
            }
        }
        // GET: Registro
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(User x)
        {
            if (ModelState.IsValid)
            {
                bool resultado = UsuarioAD.Registrar(x);

                if (resultado)
                {
                    return RedirectToAction("Login", "User");


                }
                else
                {
                    return View();
                }
            }
            else { return View(); }
        }
        //GET
        public ActionResult Consulta(string orden, string val, string search)
        {
            val = val == null ? "" : val;
            List<User> lista = UsuarioAD.Consultar();
            if (!String.IsNullOrEmpty(search)) //recorrido y busqueda 

            {
                lista = lista.Where(s => s.usuario.Contains(search)
                 || s.apellido.Contains(search) || s.direccion.Contains(search) || s.telefono.Contains(search) || s.mail.Contains(search)
                                        || s.nombre.Contains(search)).ToList();
            }
            switch (orden)
            {
                case "id_usuario":
                    if (String.IsNullOrEmpty(val) || val.Equals("id_usuarioDesc"))
                    {
                        ViewBag.val = "id_usuarioAsc";
                        lista = lista.OrderBy(s => s.usuario).ToList();
                    }
                    else if (val.Equals("id_usuarioAsc"))
                    {
                        ViewBag.val = "id_usuarioDesc";
                        lista = lista.OrderByDescending(s => s.id_usuario).ToList();
                    }
                    break;
                case "usuario":
                    if (String.IsNullOrEmpty(val) || val.Equals("usuarioDesc"))
                    {
                        ViewBag.val = "usuarioAsc";
                        lista = lista.OrderBy(s => s.usuario).ToList();
                    }
                    else if (val.Equals("usuarioAsc"))
                    {
                        ViewBag.val = "usuarioDesc";
                        lista = lista.OrderByDescending(s => s.usuario).ToList();
                    }
                    break;
                case "nombre":
                    if (String.IsNullOrEmpty(val) || val.Equals("nombreDesc"))
                    {
                        ViewBag.val = "nombreAsc";
                        lista = lista.OrderBy(s => s.nombre).ToList();
                    }
                    else if (val.Equals("nombreAsc"))
                    {
                        ViewBag.val = "nombreDesc";
                        lista = lista.OrderByDescending(s => s.nombre).ToList();
                    }
                    break;
                case "apellido":
                    if (String.IsNullOrEmpty(val) || val.Equals("apellidoDesc"))
                    {
                        ViewBag.val = "apellidoAsc";
                        lista = lista.OrderBy(s => s.apellido).ToList();
                    }
                    else if (val.Equals("apellidoAsc"))
                    {
                        ViewBag.val = "apellidoDesc";
                        lista = lista.OrderByDescending(s => s.apellido).ToList();
                    }
                    break;
                case "direccion":
                    if (String.IsNullOrEmpty(val) || val.Equals("direccionDesc"))
                    {
                        ViewBag.val = "direccionAsc";
                        lista = lista.OrderBy(s => s.direccion).ToList();
                    }
                    else if (val.Equals("direccionAsc"))
                    {
                        ViewBag.val = "direccionDesc";
                        lista = lista.OrderByDescending(s => s.direccion).ToList();
                    }
                    break;
                case "telefono":
                    if (String.IsNullOrEmpty(val) || val.Equals("telefonoDesc"))
                    {
                        ViewBag.val = "telefonoAsc";
                        lista = lista.OrderBy(s => s.telefono).ToList();
                    }
                    else if (val.Equals("telefonoAsc"))
                    {
                        ViewBag.val = "telefonoDesc";
                        lista = lista.OrderByDescending(s => s.telefono).ToList();
                    }
                    break;

                case "email":
                    if (String.IsNullOrEmpty(val) || val.Equals("emailDesc"))
                    {
                        ViewBag.val = "emailAsc";
                        lista = lista.OrderBy(s => s.mail).ToList();
                    }
                    else if (val.Equals("emailAsc"))
                    {
                        ViewBag.val = "emailDesc";
                        lista = lista.OrderByDescending(s => s.mail).ToList();
                    }
                    break;
                default:
                    lista = lista.OrderBy(s => s.usuario).ToList();
                    break;
            }


            return View(lista);
        }
        // GET: Update
        public ActionResult Update(int id_usuario)
        {
            User resultado = UsuarioAD.updateUser(id_usuario);
            ViewBag.Nombre = resultado.nombre + "" + resultado.apellido;
            return View(resultado);
        }
        // Post: Update
        [HttpPost]
        public ActionResult Update(User model)
        {
            bool resultado = UsuarioAD.Actualizar(model);
            if (resultado)
            {
                return RedirectToAction("Consulta", "User");
            }
            else

                return View(model);
        }
        // GET: Delete
        public ActionResult Delete(int id_usuario)
        {
            User resultado = UsuarioAD.updateUser(id_usuario);
            return View(resultado);
        }
        // Post: Update
        [HttpPost]
        public ActionResult Delete(User model)
        {
            bool resultado = UsuarioAD.Eliminar(model);
            if (resultado)
            {
                return RedirectToAction("Consulta", "User");
            }
            else

                return View(model);
        }



        public ActionResult ReportePDF()
        {


            //  FileStream fs = new FileStream("D://Reporte/ReporteUsuarios.pdf?font1",FileMode.Create); -- esto es por si quiero descargar automáticamente
            MemoryStream ms = new MemoryStream();

            Document documento = new Document(PageSize.A4, 30, 30, 30, 30);
            PdfWriter pw = PdfWriter.GetInstance(documento, ms);
            pw.PageEvent = new HeaderFooter();

            documento.Open();

            //Imagen

            Image imagen = Image.GetInstance("C:/Users/GINA/Desktop/escudo.png");
            imagen.ScaleAbsolute(100, 100);
            imagen.SetAbsolutePosition(documento.PageSize.Width - 36f - 80f, documento.PageSize.Height - 50f - 60f);

            documento.Add(imagen);

            imagen.SpacingBefore = 100;
            imagen.SpacingAfter = 100;

            //Fuentes y renglones de la parte de arriba (no cabecera porque no quiero que se repita en cada página)


            List<tablaConfig> listaConf = pdfConfig.Configurar(); //hago que lea cada valor del modelo 

            foreach (var elem in listaConf)
            {
                if (elem.font == true)
                {
                    Font titulo = new Font(Font.FontFamily.COURIER, 14, Font.BOLD, BaseColor.BLACK);
                    Font frase1 = new Font(Font.FontFamily.COURIER, 12, Font.BOLD, BaseColor.LIGHT_GRAY);
                    Font frase2 = new Font(Font.FontFamily.COURIER, 10, Font.BOLD, BaseColor.PINK);
                    Font contacto = new Font(Font.FontFamily.COURIER, 8, Font.BOLD, BaseColor.BLUE);
                    documento.Add(new Paragraph("Reporte de Usuarios", titulo));
                    documento.Add(new Paragraph("Municipalidad de Beach City, el hogar de Steven Universe", frase1));
                    documento.Add(new Paragraph("Beach City - Somewhere on Planet Earth", frase2));
                    documento.Add(new Paragraph("contacto: https://worldofsteven.com/", contacto));
                }
                if (elem.font == false)
                {
                    Font titulo = new Font(Font.FontFamily.COURIER, 14, Font.BOLD, BaseColor.BLACK);

                    Font frase1 = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.LIGHT_GRAY);
                    Font frase2 = new Font(Font.FontFamily.UNDEFINED, 10, Font.ITALIC, BaseColor.PINK);
                    Font contacto = new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.BOLDITALIC, BaseColor.BLUE);
                    documento.Add(new Paragraph("Reporte de Usuarios", titulo));
                    documento.Add(new Paragraph("Municipalidad de Beach City, el hogar de Steven Universe", frase1));
                    documento.Add(new Paragraph("Beach City - Somewhere on Planet Earth", frase2));
                    documento.Add(new Paragraph("contacto: https://worldofsteven.com/", contacto));
                }







                //    if { documento.Add(new Paragraph("PRUEBAPRUEBAPRUEBAPRUEBA")); }




                Chunk linebreak = new Chunk("\n");

                documento.Add(linebreak);
                documento.Add(linebreak);
                documento.Add(linebreak);
                documento.Add(linebreak);

                // Línea divisoria 

                LineSeparator underline = new LineSeparator(1, 100, BaseColor.BLACK, Element.ALIGN_CENTER, -2);
                documento.Add(underline);
                documento.Add(linebreak);
                PdfPTable tabla = new PdfPTable(6);

                tabla.SpacingAfter = 100;
                tabla.SpacingBefore = 20;

                //Declaración estilo de Tabla 
                if (elem.color == true)
                {
                    Font titulotabla = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.PINK);

                    tabla.AddCell(new Paragraph("Usuario", titulotabla));
                    tabla.AddCell(new Paragraph("Nombre", titulotabla));
                    tabla.AddCell(new Paragraph("Apellido", titulotabla));
                    tabla.AddCell(new Paragraph("Dirección", titulotabla));
                    tabla.AddCell(new Paragraph("Teléfono", titulotabla));
                    tabla.AddCell(new Paragraph("Mail", titulotabla));
                }
                else
                {
                    Font titulotabla = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK);

                    tabla.AddCell(new Paragraph("Usuario", titulotabla));
                    tabla.AddCell(new Paragraph("Nombre", titulotabla));
                    tabla.AddCell(new Paragraph("Apellido", titulotabla));
                    tabla.AddCell(new Paragraph("Dirección", titulotabla));
                    tabla.AddCell(new Paragraph("Teléfono", titulotabla));
                    tabla.AddCell(new Paragraph("Mail", titulotabla));
                }


                List<User> lista = UsuarioAD.Consultar();
                foreach (var element in lista)
                {
                    tabla.AddCell(new Paragraph(element.usuario));
                    tabla.AddCell(new Paragraph(element.nombre));
                    tabla.AddCell(new Paragraph(element.apellido));
                    tabla.AddCell(new Paragraph(element.direccion));
                    tabla.AddCell(new Paragraph(element.telefono));
                    tabla.AddCell(new Paragraph(element.mail));

                }
                documento.Add(tabla); // agrego la tabla al documento

                if (elem.size == true)
                {
                    Font cookie = new Font(Font.FontFamily.HELVETICA, 20, Font.ITALIC, BaseColor.PINK);

                    documento.Add(new Paragraph("Oh, he's a frozen treat with an all new taste, Cause he came to this planet from outer space. A refugee of an interstellar war, But now he's at your local grocery store! Cookie Cat (He's a pet for your tummy) " +
                        "Cookie Cat (He's super duper yummy!) Cookie Cat He left his family behind :'C  Cookie Caaat!", cookie));
                }
                else
                {
                    Font cookie = new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC, BaseColor.PINK);

                    documento.Add(new Paragraph("Oh, he's a frozen treat with an all new taste, Cause he came to this planet from outer space. A refugee of an interstellar war, But now he's at your local grocery store! Cookie Cat (He's a pet for your tummy) " +
                        "Cookie Cat (He's super duper yummy!) Cookie Cat He left his family behind :'C  Cookie Caaat!", cookie));
                }

                documento.Add(linebreak);
                documento.Add(linebreak);
                documento.Add(linebreak);
                documento.Add(linebreak);
                documento.Add(linebreak);

                //Firma 

                LineSeparator firma = new LineSeparator(1, 30, BaseColor.BLACK, Element.ALIGN_LEFT, -2);
                Chunk linebreak2 = new Chunk();
                Font firmayac = new Font(Font.FontFamily.HELVETICA, 6, Font.ITALIC, BaseColor.BLACK);
                documento.Add(firma);

                documento.Add(new Paragraph("Firma y Aclaración", firmayac));

                documento.Close();  //Se cierra el documento

                byte[] bytesStream = ms.ToArray();   //Esto es para que el PDF se abra en una pestaña, y de ahí que se pueda descargar. Lo crea en memoria

                ms = new MemoryStream();
                ms.Write(bytesStream, 0, bytesStream.Length);
                ms.Position = 0;
            }
                return new FileStreamResult(ms, "application/pdf");
            }
        
        class HeaderFooter : PdfPageEventHelper //nueva clase para poner el footer en cada página
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            { //Header que no voy a hacer por ahora
                /*PdfPTable tbHeader = new PdfPTable(3);
                tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbHeader.DefaultCell.Border = 0;

                tbHeader.AddCell(new Paragraph());
                tbHeader.AddCell(new Paragraph());
                tbHeader.AddCell(new Paragraph());*/

                PdfPTable tbFooter = new PdfPTable(2);
                tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tbFooter.DefaultCell.Border = 0;

                Font footer = new Font(Font.FontFamily.COURIER, 8, Font.NORMAL, BaseColor.DARK_GRAY);

                PdfPCell celda1 = new PdfPCell(new Paragraph("User: Pink Diamond - Terminal:  4.8.15.16.23.42", footer));
                celda1.HorizontalAlignment = Element.ALIGN_LEFT;
                celda1.Border = 0;
                tbFooter.AddCell(celda1);
                tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 20, writer.DirectContent);


                PdfPCell celda2 = new PdfPCell(new Paragraph("Página " + writer.PageNumber));
                celda2.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda2.Border = 0;
                tbFooter.AddCell(celda2);
                tbFooter.WriteSelectedRows(0, -1, document.RightMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5, writer.DirectContent);
            }
        }
    }
}
