using BloodBankCare.Helpers;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Loader;
using System.Reflection;

namespace BloodBankCare.Controllers
{
    public class TestController : Controller
    {
        private readonly MyPDF myPDF;
        private readonly string rootPath;

        public TestController(IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JqueryAutocomplete()
        {
            return View();
        }

        public IActionResult Accordian()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult TestReportPdf()
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Home/Index";

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
