namespace Firma.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Requisicion_TicketDetail
    {
        [Key]
        [StringLength(100)]
        public string Ticket { get; set; }

        [StringLength(100)]
        public string Solicitado_Por { get; set; }

        [StringLength(100)]
        public string Cuenta_Cargar { get; set; }

        [StringLength(100)]
        public string Tipo_Gastos { get; set; }

        [StringLength(255)]
        public string Dep { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }

        public string Justificacion { get; set; }

        [StringLength(100)]
        public string Recibidox_txt { get; set; }

        [StringLength(100)]
        public string Verificadox_txt { get; set; }

        [StringLength(100)]
        public string Entregadox_txt { get; set; }

        public DateTime? FH_Despachado { get; set; }

        public string Recibidox_F { get; set; }

        public string Verificadox_F { get; set; }

        public string Entregadox_F { get; set; }

        public decimal Total { get; set; }
    }
}
