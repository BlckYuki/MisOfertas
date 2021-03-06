﻿using CapaDTO;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MisOfertas.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdf
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post()
        {
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

           

            

            //TEXTO DEL HEADING
            Chunk chunk = new Chunk("MIS OFERTAS", FontFactory.GetFont("Arial", 20, Font.BOLDITALIC, BaseColor.DARK_GRAY));
            pdfDoc.Add(chunk);

            //Horizontal Line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);

            //Table
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            //0=Left, 1=Centre, 2=Right
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            //IMAGEN DEL CODIGO
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            Image image = Image.GetInstance(Server.MapPath("~/Content/Upload/CodigoQR.png"));
            image.ScaleAbsolute(200, 150);
            cell.AddElement(image);
            table.AddCell(cell);


            Usuario usu = (Usuario)Session["loginUsuario"];
           // NegocioUsuario negocioUsuario = new NegocioUsuario();
            
              //  Usuario usuario = negocioUsuario.retornaUsuario(idUsuario);
           
            
            
            // Session["idUsuario"] = usuario.IdUsuario;
            
            //Cell no 2
            chunk = new Chunk("Nombre:"+ usu.Nombre+ " ,\nRut:"+ usu.Rut+" , \nDireccion:"+usu.Direccion+" , \nTelefono:"+usu.Telefono+" , \nFecha: ", FontFactory.GetFont("Arial", 15, Font.NORMAL, BaseColor.BLACK));
            

            cell = new PdfPCell();
            cell.Border = 0;
            cell.AddElement(chunk);
            table.AddCell(cell);

            //Add table to document
            pdfDoc.Add(table);

            //Horizontal Line
            line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);


            //creador pdf importante
            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=MisOfertasProducto.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            return View();
        }

        [HttpPost]
        public ActionResult Reset()
        {
            return RedirectToAction("Index");
        }
    }
}