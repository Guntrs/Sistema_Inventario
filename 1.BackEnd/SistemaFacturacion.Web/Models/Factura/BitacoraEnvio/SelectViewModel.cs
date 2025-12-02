using System;

namespace SistemaFacturacion.Web.Models.Factura.BitacoraEnvio
{
    public class SelectViewModel
    {
        public int IdBitacora { get; set; }

        public DateTime FechaEnvio { get; set; } = DateTime.Now;
        public string RespuestaSAT { get; set; }
        public string CodigoRespuestaSAT { get; set; }
        public string MensajeRespuestaSAT { get; set; }
    }
}
