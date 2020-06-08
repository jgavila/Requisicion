namespace Firma.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class ModelMain : DbContext
    {
        public ModelMain()
            : base("name=ModelMain")
        {
        }

        public virtual DbSet<Requisicion_ProductDetail> Requisicion_ProductDetail { get; set; }
        public virtual DbSet<Requisicion_TicketDetail> Requisicion_TicketDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Requisicion_ProductDetail>()
                .Property(e => e.Ticket)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_ProductDetail>()
                .Property(e => e.Id_producto)
                .IsFixedLength();

            modelBuilder.Entity<Requisicion_ProductDetail>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Ticket)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Solicitado_Por)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Cuenta_Cargar)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Tipo_Gastos)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Dep)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Justificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Recibidox_txt)
                .IsFixedLength();

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Verificadox_txt)
                .IsFixedLength();

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Entregadox_txt)
                .IsFixedLength();

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Recibidox_F)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Verificadox_F)
                .IsUnicode(false);

            modelBuilder.Entity<Requisicion_TicketDetail>()
                .Property(e => e.Entregadox_F)
                .IsUnicode(false);
        }

        public List<SP_ShowTiecktDetailResult> SP_ShowTiecktDetail(string ticket)
        {
            
                return this.Database.SqlQuery<SP_ShowTiecktDetailResult>("EXEC [dbo].[SP_ShowTiecktDetail] @ticket = @p0", ticket).ToList();
            
        }

        public List<SP_ShowProductDetailResult> SP_ShowProductDetail(string ticket)
        {
                return this.Database.SqlQuery<SP_ShowProductDetailResult>("EXEC [dbo].[SP_ShowProductDetail] @ticket = @p0", ticket).ToList();
          
        }

      

    }
}
