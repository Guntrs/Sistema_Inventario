using System;

namespace SistemaFacturacion.Web.Models.Factura.BitacoraEnvio
{
    public class CrearViewModel
    {
     
        public int IdVenta { get; set; }

        public DateTime FechaEnvio { get; set; } 
        public string RespuestaSAT { get; set; }
        public string CodigoRespuestaSAT { get; set; }
        public string MensajeRespuestaSAT { get; set; }
    }
}
