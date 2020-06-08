using Firma.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firma.Controllers
{
    public class MRebajaController : Controller
    {
        //private ModelMain db = new ModelMain();
        CWRevolutionEntities db = new CWRevolutionEntities();

        // GET: MRebaja
        public ActionResult Index()
        {
            //ViewBag.validate = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(DateTime finicio, DateTime ffinal, string list, string dgasto, string ccargar)
        {


            ViewBag.validate = 1;
            ViewBag.finicio = finicio;
            ViewBag.ffinal = ffinal;

            ViewBag.list = list;
            ViewBag.dgasto = dgasto;
            ViewBag.ccargar = ccargar;

            decimal total = 0;
            
            var detail = db.SP_Req_MRebaja(finicio, ffinal,list);
            foreach (var item in detail)
            {
                
                item.Costo_Total = decimal.Round(item.Costo_Total, 2);
                total += item.Costo_Total;

            }
            ViewBag.Total = total;
            
            return View(db.SP_Req_MRebaja(finicio, ffinal,list));
            
        }

        public class Header
        {
            public DateTime Finicio { get; set; }
            public DateTime Ffinal { get; set; }
            public string List { get; set; }
            public string Dgasto { get; set; }
            public string Ccargar { get; set; }
            public decimal Total { get; set; }

        }

        public String generarPrimaryKey()
        {
            String res = "";
            int cod = 4000;

            try
            {
                DbQuery<Req_Rebaja_Header> query = db.Set<Req_Rebaja_Header>();
                List<Req_Rebaja_Header> lista = query.ToList();

                // si hay algo en la tabla lo trae extrae el numero y le suma uno
                if (lista.Count() > 0)
                {
                    string codigo = lista.LastOrDefault().Ncons;
                    cod = Convert.ToInt16(codigo.Substring(2));
                    cod = cod + 1;
                }

                // arma el codigo concatenando BR con el numero generado
                res = "BR" + cod;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return res;
        }



        public ActionResult SaveInfo(Header header)
        {
            try
            {

                var theader = new Req_Rebaja_Header();

                string Ncons = generarPrimaryKey();

                theader.Ncons = Ncons;
                theader.FechaIn = header.Finicio;
                theader.FechaFi = header.Ffinal;
                theader.Departamento = header.List;
                theader.Detalle_Gasto = header.Dgasto;
                theader.Cuenta_Cargar = header.Ccargar;
                theader.Fecha_Rep = DateTime.Now;
                theader.Total = header.Total;

                db.Req_Rebaja_Header.Add(theader);
                Session["Ncons"] = theader.Ncons;


                var detail = db.SP_Req_MRebaja(header.Finicio, header.Ffinal,header.List);
                foreach (var item in detail)
                {
                    var tabla = new Req_Rebaja_Detail();
                    tabla.Ncons = Ncons;
                    tabla.Stringkey = item.Stringkey;
                    tabla.ticket = item.C__de_Ticket.ToString();
                    tabla.Cliente = item.Cliente;
                    tabla.Num_Parte = item.Item_Id;
                    tabla.Descripcion = item.Descripcion_Producto;
                    tabla.Bodega_Rebaja = item.Bodega;
                    tabla.Cant_Solicitada = Convert.ToInt32(item.Cantidad);
                    tabla.Cant_Despachada = item.Quantity_Shipped;
                    tabla.Costo_Unitario = item.Costo_Unitario;
                    tabla.Costo_Total = item.Costo_Total;
                    tabla.Fecha_Shipping = item.Fecha_Shipping;
                    tabla.Hecho_Por = "Admin";
                    tabla.Contrato = item.Contrato;
                    tabla.Proyecto = item.Proyecto;
                    tabla.Bin = item.Bin;
                    

                    db.Req_Rebaja_Detail.Add(tabla);
                }

                db.SaveChanges();
                Descarga();
                
                
                return Json(new { rtn = "Datos guardado" });

            }
            catch (Exception e)
            {

                return Json(new { rtn = e.Message });
            }

        }

        [HttpPost]
        public ActionResult Descarga()
        {
            var ncons = Session["Ncons"];
            System.Diagnostics.Process.Start("http://ptycwclsql/ReportServer_CWISEARIS/Pages/ReportViewer.aspx?%2fCustom%2fREQ_REBAJA_SSRS&rs:Command=Render&Ncons=" + ncons + "&rs:Format=EXCEL");
            

            return View();

        }



    }
}