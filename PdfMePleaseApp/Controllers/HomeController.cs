using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PdfMePleaseApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));

                    ConversionResult cResult = Code.Convert.ToPdf(file, Server.MapPath("~/Temp"));                    

                    ViewBag.Message = "Get you pdf here";
                    ViewBag.Link = $"./Temp/{cResult.FileName}";
                    ViewBag.FileName = cResult.FileName;
               } 
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }
    }
}