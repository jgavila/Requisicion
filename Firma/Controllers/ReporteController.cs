using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firma.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index( string finicio, string ffinal)
        {
            var url = "http://ptycwclsql/ReportServer_CWISEARIS/Pages/ReportViewer.aspx?%2fCustom%2fReporte_diario_requisicion&rs:Command=Render&Finicio=" + finicio + " &Ffinal=" + ffinal + "&rs:Format=Excel";
            Response.Redirect(url);
            
            return View();
             
        }


    }
}