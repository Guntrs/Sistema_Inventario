using SistemaFacturacion.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SistemaFacturacion.Entidades.Usuarios;

namespace SistemaFacturacion.Entidades.Factura
{
    public class BitacoraEnvio
    {
        [Key]
        public int IdBitacora { get; set; }

        public int IdVenta { get; set; }

        public DateTime FechaEnvio { get; set; } 
        public string RespuestaSAT { get; set; }
        public string CodigoRespuestaSAT { get; set; }
        public string MensajeRespuestaSAT { get; set; }


        //referencia de la tabla Usuario
        [Required]
        [ForeignKey("IdVenta")]
        public Venta ventas { get; set; }
    }
}
