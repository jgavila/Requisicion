namespace Firma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Requisicion_ProductDetail
    {
        [Key, Column(Order = 0)]
        [StringLength(100)]
        public string Ticket { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(50)]
        public string Id_producto { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        public decimal Cant_solicitada { get; set; }

        public decimal Costo_Unitario { get; set; }

        public decimal Despachados { get; set; }

        public decimal Costo_Total { get; set; }

       
    }
}
