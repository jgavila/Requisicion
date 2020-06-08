using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Firma.Models
{
    public class SP_Req_MRebaja_Result
    {
       
        public string Ticket { get; set; }
        public string Cliente { get; set; }
       
        public string Item_Id { get; set; }
        public string Descripcion_Producto { get; set; }
        public string Departamento { get; set; }

        public decimal Cantidad { get; set; }

        public decimal Costo_Unitario { get; set; }
        public decimal Costo_Total { get; set; }


        //public decimal Costo_Total { get; set; }
        //public decimal Total { get; set; }


    }
}