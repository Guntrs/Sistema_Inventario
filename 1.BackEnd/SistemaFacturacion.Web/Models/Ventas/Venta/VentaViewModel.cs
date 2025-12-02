using System;

namespace SistemaFacturacion.Web.Models.Ventas.Venta
{
    public class VentaViewModel
    {
        public int IdVenta { get; set; }
        //----------------------------------
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        //----------------------------------
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        //----------------------------------
        public int IdCaja { get; set; }
        public string DescripcionCaja { get; set; }


        public int IdComprobante { get; set; }
        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public int Correlativo { get; set; }

        //-------------------------------------
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }
        //-------------------------------------

        public DateTime FechaHora { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        //-------------------------------------

        public Guid UUID { get; set; } = Guid.NewGuid();
        public string XMLFactura { get; set; }
        public string SerieSAT { get; set; }

        public int? NumeroSAT { get; set; }
    }
}
