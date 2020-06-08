using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firma.Controllers
{
    public class MDespController : Controller
    {
        CWRevolutionEntities db = new CWRevolutionEntities();

        // GET: MDesp
        public ActionResult Index()
        {
            
            Dropdownlist();
            return View();
            
        }
      
        [HttpPost]
        public ActionResult Guardar(Req_Desp_Header array1, List<Req_Desp_Detail> array2)
        {
            Dropdownlist();
            string Ncons = GenerarPrimaryKey();

            var header = new Req_Desp_Header
            {
                Ncons = Ncons,
                Tipo_const = array1.Tipo_const,
                Solicitado_por = array1.Solicitado_por,
                Departamento = array1.Departamento,
                Sucursal = array1.Sucursal,
                Desde = array1.Desde,
                Hacia = array1.Hacia,
                Referencias = array1.Referencias,
                Notas_inventario = array1.Notas_inventario,
                Entregado_por = array1.Entregado_por,
                Verificado_por = array1.Verificado_por,
                Recibido_por = array1.Recibido_por,
                TxtEntregadox = array1.TxtEntregadox,
                TxtRecibidox = array1.TxtRecibidox,
                TxtVerificadox = array1.TxtVerificadox,
                Fecha = DateTime.Now,
                Total = array1.Total,

            };

            db.Req_Desp_Header.Add(header);

            foreach (var item in array2)
            {
                var prod = new Req_Desp_Detail
                {
                    Ncons = Ncons,
                    Num_parte = item.Num_parte,
                    Descripcion = item.Descripcion,
                    Num_serie = item.Num_serie,
                    Cant_solic = item.Cant_solic,
                    Cant_desp = item.Cant_desp,
                    Costo = item.Costo,
                    Totaltbl = item.Totaltbl
                };

                db.Req_Desp_Detail.Add(prod);
            }

            db.SaveChanges();
            return Json(new { rtn = "Info ha sido guardado" });

        }

        public String GenerarPrimaryKey()
        {
            String res = "";
            int cod = 4000;

            try
            {
                DbQuery<Req_Desp_Header> query = db.Set<Req_Desp_Header>();
                List<Req_Desp_Header> lista = query.ToList();

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
        
        public void Dropdownlist()
        {
            var tconstlist = new List<SelectListItem>
            {
                 new SelectListItem{ Text="Transferencia de bodega", Value = "Transferencia de bodega" , Selected = true },
                 new SelectListItem{ Text="Constancia de salida", Value = "Constancia de salida" },
                 new SelectListItem{ Text="Cargar al archivo", Value = "Cargar al archivo"},

                };
            ViewData["tconstDropDown"] = tconstlist;

            var sucursal = new List<SelectListItem>
                {
                     new SelectListItem{ Text="TEP", Value = "TEP", Selected = true },
                     new SelectListItem{ Text="TED", Value = "TED"},
                     new SelectListItem{ Text="TEC", Value = "TEC"},

                };
            ViewData["sucursalDropDown"] = sucursal;

            var desde = new List<SelectListItem>
                {
                     new SelectListItem{ Text="Bodega TEP", Value = "Bodega TEP", Selected = true },
                     new SelectListItem{ Text="Bodega Tocumen", Value = "Bodega Tocumen"},

                };
            ViewData["desdeDropDown"] = desde;

            var hacia = new List<SelectListItem>
                {
                     new SelectListItem{ Text="TED", Value = "TED", Selected = true },
                     new SelectListItem{ Text="TEC", Value = "TEC"},
                     new SelectListItem{ Text="Bodega Tocumen", Value = "Bodega Tocumen"},

                };
            ViewData["haciaDropDown"] = hacia;

            var dept = new List<SelectListItem>
                {
                     new SelectListItem{ Text="Administración de Poryectos", Value = "Administración de Poryectos", Selected = true },
                     new SelectListItem{ Text="Administración Local", Value = "Administración Local"},
                     new SelectListItem{ Text="Alta Disponibilidad", Value = "Alta Disponibilidad"},

                };
            ViewData["deptDropDown"] = dept;

            var TxtEntregadox = new List<SelectListItem>
                {
                     new SelectListItem{ Text="Johann Credidio", Value = "Johann Credidio"},
                     new SelectListItem{ Text="Alvin Headley", Value = "Alvin Headley"},
                     new SelectListItem{ Text="Kevin Navarro", Value = "Kevin Navarro"},
                     new SelectListItem{ Text="Nelson Cardenas", Value = "Nelson Cardenas"},
                     new SelectListItem{ Text="Isaac Vargas", Value = "Isaac Vargas"},
                     new SelectListItem{ Text="Diomedes Peralta", Value = "Diomedes Peralta"},
                      
                };
            ViewData["TxtEntregadoxDropDown"] = TxtEntregadox;

            var TxtVerificadox = new List<SelectListItem>
                {
                     new SelectListItem{ Text="Johann Credidio", Value = "Johann Credidio"},
                     new SelectListItem{ Text="Alvin Headley", Value = "Alvin Headley"},
                     new SelectListItem{ Text="Yasmina Ortega", Value = "Yasmina Ortega"},
                     new SelectListItem{ Text="Janeth Villamil", Value = "Janeth Villamil"},
                     
                };
            ViewData["TxtVerificadoxDropDown"] = TxtVerificadox;


        }

      
    }
}