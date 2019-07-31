using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Models
{
    public class SP_ShowTiecktDetailResult
    {
        //[Required(ErrorMessage = "Please enter ticket number")]
        public string Ticket { get; set; }
        public string Solicitado_Por { get; set; }
        public string Cuenta_Cargar { get; set; }
        public string Tipo_Gasto { get; set; }
        public string Departarmento { get; set; }
        public DateTime Fecha { get; set; }
        public string Justificacion { get; set; }

        
    }
}