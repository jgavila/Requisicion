//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Firma
{
    using System;
    using System.Collections.Generic;
    
    public partial class Req_Rebaja_Detail
    {
        public int id { get; set; }
        public string Ncons { get; set; }
        public string Stringkey { get; set; }
        public string ticket { get; set; }
        public string Cliente { get; set; }
        public string Num_Parte { get; set; }
        public string Descripcion { get; set; }
        public string Bodega_Rebaja { get; set; }
        public Nullable<int> Cant_Solicitada { get; set; }
        public Nullable<int> Cant_Despachada { get; set; }
        public Nullable<decimal> Costo_Unitario { get; set; }
        public decimal Costo_Total { get; set; }
        public Nullable<System.DateTime> Fecha_Shipping { get; set; }
        public string Hecho_Por { get; set; }
        public string Contrato { get; set; }
        public string Proyecto { get; set; }
        public string Bin { get; set; }
    }
}