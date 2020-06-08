using Firma.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Firma.Controllers
{
    public class RequisicionController : Controller
    {
        private ModelMain db = new ModelMain();

        public class Datos
        {
            public string ticket { get; set; }
            public string recibido_firma { get; set; }
            public string verificado_firma { get; set; }
            public string entregado_firma { get; set; }
            public string recibido_por { get; set; }
            public string verificado_por { get; set; }
            public string entregado_por { get; set; }
        }


        public ActionResult VerificarTicket(int ticket)
        {

            using (var dbContext = new ModelMain())
            {

                var ticketString = ticket.ToString();

                var listaTicket = dbContext.SP_ShowTiecktDetail(ticketString);

                //SI  EXISTE EN EL SP
                if (listaTicket.Count > 0)
                {
                    //
                    //aqui buscamos el ticket en la base,
                    var detalleTicket = dbContext.Requisicion_TicketDetail.Where(x => x.Ticket == ticketString).Select(x => x).ToList();

                    // si encontró el ticket EN LA BASE QUE GUARDA entonces retorna false ( ya está despacahado )
                    if (detalleTicket.Count > 0)
                    {
                        //YA NO DEBE AVANZAR A LA PAGINA


                        return Json("Reporte " + ticket + " ya ha sido despachado.");
                    }
                    else
                    {
                        //aqui hay que validad que despachado solamente debe mostrar si es mayor a 0 

                        var fila = dbContext.SP_ShowProductDetail(ticketString);

                        if (fila.Count == 0)
                        {

                            return Json("El reporte " + ticket + " no tiene productos despachados.");
                        }

                        //hay en el sp pero no en la base , entonces si debe dejar acvanzar 
                        return Json("");
                    }
                }
                else
                {
                    //no existe en el SP, retorna false
                    return Json("Reporte " + ticket + " no existe.");
                }
            }

        }

        public ActionResult Index(string ticket)
        {

            if (!String.IsNullOrWhiteSpace(ticket))
            {
                ViewBag.ticketDetail = db.SP_ShowTiecktDetail(ticket).FirstOrDefault();
                var datosTemp = db.SP_ShowProductDetail(ticket);

                decimal totalFinal = 0;
                foreach (var dato in datosTemp)
                {
                    dato.Cant_Solicitada = (Int32)dato.Cant_Solicitada;
                    dato.Despachado = (Int32)dato.Despachado;
                    //dato.Costo_Total = decimal.Parse(dato.Cost_Unitario) * dato.Despachado;
                    dato.Costo_Total = dato.Cost_Unitario * dato.Despachado;
                    totalFinal += dato.Costo_Total;
                    dato.Costo_Total = decimal.Round(dato.Costo_Total, 2);
                }


                ViewBag.productDetail = datosTemp;

                ViewBag.Total = totalFinal;
                ViewBag.ticket = ticket;
            }

            return View();

        }

        public ActionResult GuardarDatos(Datos datos)
        {
            try
            {
                var ticketDetail = db.SP_ShowTiecktDetail(datos.ticket).FirstOrDefault();
                var productDetail = db.SP_ShowProductDetail(datos.ticket);

                var detail = new Requisicion_TicketDetail();
                //Asignar los valores al nuevo registro
                detail.Solicitado_Por = ticketDetail.Solicitado_Por;
                detail.Cuenta_Cargar = ticketDetail.Cuenta_Cargar;
                detail.Tipo_Gastos = ticketDetail.Tipo_Gasto;
                detail.Dep = ticketDetail.Departarmento;
                detail.Fecha = Convert.ToDateTime(ticketDetail.Fecha);
                detail.Justificacion = ticketDetail.Justificacion;

                detail.Ticket = datos.ticket;
                detail.Recibidox_F = datos.recibido_firma;
                detail.Recibidox_txt = datos.recibido_por;
                detail.Verificadox_F = datos.verificado_firma;
                detail.Verificadox_txt = datos.verificado_por;
                detail.Entregadox_F = datos.entregado_firma;
                detail.Entregadox_txt = datos.entregado_por;
                detail.FH_Despachado = DateTime.Now;

                var datosTempg = db.SP_ShowProductDetail(datos.ticket);
                decimal tsuma = 0;
                foreach (var dato in datosTempg)
                {

                    //dato.Costo_Total = decimal.Parse(dato.Cost_Unitario) * dato.Despachado;
                    dato.Costo_Total = dato.Cost_Unitario * dato.Despachado;
                    tsuma += dato.Costo_Total;
                    tsuma += dato.Costo_Total;

                }
                detail.Total = tsuma;

                //Agregar el registro
                db.Requisicion_TicketDetail.Add(detail);


                foreach (var item in productDetail)
                {
                    var prod = new Requisicion_ProductDetail();
                    //Asignar los valores al nuevo registro
                    prod.Ticket = datos.ticket;
                    prod.Id_producto = item.ID_Producto;
                    prod.Descripcion = item.Descripcion;
                    prod.Cant_solicitada = item.Cant_Solicitada;
                    prod.Costo_Unitario = item.Cost_Unitario;
                    prod.Despachados = Convert.ToInt32(item.Despachado);
                    //prod.Costo_Total = decimal.Parse(prod.Costo_Unitario) * prod.Despachados;
                    prod.Costo_Total = prod.Costo_Unitario * prod.Despachados;

                    //Agregar el registro

                    db.Requisicion_ProductDetail.Add(prod);
                }

                //Guardar los cambios (esto ejecuta los insert o update necesarios)
                db.SaveChanges();
                return Json(new { rtn = "Reporte de requisicion ha sido guardado" });
            }
            catch (Exception e)
            {
                return Json(new { rtn = e.Message });
            }

        }

        public async Task<ActionResult> Reporte(string ticket)
        {
            var parametros = new List<KeyValuePair<string, string>>();
            parametros.Add(new KeyValuePair<string, string>("ticket", ticket));
            //El primer parametro es la ruta del reporte
            //El segundo parametro es el nombre del archivo (como se quiera descargar)
            // return await this.Render("Reportes+ATM%2fRequisicion", "Requisicion", "PDF", parametros);
            return await this.Render("Custom%2fRequisicion", "Requisicion", "PDF", parametros);

        }

        /// <summary>
        /// Metodo para obtener el reporte desde Reporting Server
        /// </summary>
        /// <param name="ruta">Ruta del reporte ejemplo: carpeta1/reporte1</param>
        /// <param name="fileName">Nombre del archivo al descargar</param>
        /// <param name="format">Formato: PDF o EXCEL</param>
        /// <param name="parametros">Lista de parametros a enviar para obtener el reporte</param>
        /// <returns>Regresa el archivo o en caso de error un Json</returns>
        private async Task<ActionResult> Render(string ruta, string fileName, string format, List<KeyValuePair<string, string>> parametros)
        {
            try
            {
                //Credenciales para acceder al Reporting Server
                //var credentials = new NetworkCredential("javila", "*****", "TECNASA");
                //var handler = new HttpClientHandler { Credentials = credentials };
                HttpClientHandler handler = new HttpClientHandler();
                handler.UseDefaultCredentials = true;

                using (var client = new System.Net.Http.HttpClient(handler))
                {
                    //Ruta del Reporting Server
                    var url = "http://ptycwclsql/ReportServer_CWISEARIS/Pages/ReportViewer.aspx?/"
                        + ruta
                        + "&rs:Command=Render&rs:Format="
                        + format;
                    foreach (var p in parametros)
                    {
                        url += "&" + p.Key + "=" + p.Value;
                    }
                    using (var result = await client.GetAsync(url))
                    {
                        if (result.IsSuccessStatusCode)
                        {
                            var buffer = await result.Content.ReadAsByteArrayAsync();
                            var content = new System.IO.MemoryStream(buffer);
                            var contentType = "application/octet-stream";
                            if (format.ToUpper() == "EXCEL")
                            {
                                fileName += ".xls";
                                contentType = "application/vnd.ms-excel";
                            }
                            else
                            {
                                fileName += ".pdf";
                                contentType = "application/pdf";

                            }
                            return File(content, contentType, fileName);

                        }

                    }
                }
                return HttpNotFound();
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al intentar generar el reporte.", message = ex.GetBaseException().Message }, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
