using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Models
{
    public class SP_ShowProductDetailResult
    {
       
        public string Ticket { get; set; }
        public string ID_Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cant_Solicitada { get; set; }

        public string Cost_Unitario { get; set; }
        public decimal Despachado { get; set; }
        public decimal Costo_Total { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Total { get; set; }
    }
}