using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PdfMePleaseApp.Code
{
    public class Convert
    {
        public int FileName { get; set; }
        public static ConversionResult ToPdf(HttpPostedFileBase file, string path)
        {
            var documentConverter = new Mammoth.DocumentConverter();
            var result = documentConverter.ConvertToHtml(file.InputStream);

            string fileName = file.FileName.Replace(".", "") + Guid.NewGuid().ToString().Substring(0, 6) + ".pdf";

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(result.Value);
            doc.Save(path + $"/{fileName}");
            doc.Close();           

            return new ConversionResult() { FileName = fileName };
        }
    }
}