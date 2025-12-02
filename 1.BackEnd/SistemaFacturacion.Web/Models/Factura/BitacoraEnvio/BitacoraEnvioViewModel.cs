using System.ComponentModel.DataAnnotations;
using System;

namespace SistemaFacturacion.Web.Models.Factura.BitacoraEnvio
{
    public class BitacoraEnvioViewModel
    {
      
        public int IdBitacora { get; set; }
        //------------------------------------------
        public int IdVenta { get; set; }
        public decimal Total { get; set; }
        public string SerieSAT { get; set; }
        public int? NumeroSAT { get; set; }
        public string Estado { get; set; }
        //------------------------------------------


        public DateTime FechaEnvio { get; set; }
        public string RespuestaSAT { get; set; }
        public string CodigoRespuestaSAT { get; set; }
        public string MensajeRespuestaSAT { get; set; }
    }
}
