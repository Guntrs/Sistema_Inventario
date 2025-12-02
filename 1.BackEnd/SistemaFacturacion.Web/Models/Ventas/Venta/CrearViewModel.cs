using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Ventas.Venta
{
    public class CrearViewModel
    {

        //Propiedades maestro

        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdCaja { get; set; }
        public int IdComprobante { get; set; }



        //tabla TipoComprobante
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }

        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        //-------------------------------------

        public Guid UUID { get; set; } = Guid.NewGuid();
        public string XMLFactura { get; set; }
        public string SerieSAT { get; set; }

        public int? NumeroSAT { get; set; }


        //Propiedades detalle        
        public List<DetalleViewModel> detalles { get; set; }
    }

}
