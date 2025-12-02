using SistemaFacturacion.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaFacturacion.Entidades.Factura
{
    public class CertificadoDigital
    {
        [Key]
        public int IdCertificado { get; set; }
        public int IdUsuario { get; set; }


        public string Nombre { get; set; }
        public DateTime FechaExpiracion { get; set; }   
        public byte[] ArchivoCertificado { get; set; }  // Contenido del certificado digital en formato binario
        public DateTime FechaCreacion { get; set; } 



        //referencia de la tabla Usuario
        [Required]
        [ForeignKey("IdUsuario")]
        public Usuario usuario { get; set; }


    }
}