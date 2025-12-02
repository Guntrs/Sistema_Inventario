using System;

namespace SistemaFacturacion.Web.Models.Factura.CertificadoDigital
{
    public class SelectViewModel
    {
        public int IdCertificado { get; set; }
       
        public string Nombre { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public byte[] ArchivoCertificado { get; set; }  // Contenido del certificado digital en formato binario
        public DateTime FechaCreacion { get; set; }
    }
}
