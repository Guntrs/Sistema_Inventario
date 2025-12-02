using SistemaFacturacion.Entidades.Sucursal;
using SistemaFacturacion.Entidades.Usuarios;
using SistemaFacturacion.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Ingreso
    {
        [Key]
        public int IdIngreso { get; set; }
  
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public int IdCaja { get; set; }
        public int IdComprobante { get; set; }


        //pendientes modificacion
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }    
        public int NumeroComprobante { get; set; }
        public decimal Impuesto { get; set; }


        public DateTime FechaHora { get; set; }           
        public decimal Total { get; set; }       
        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }


        //para la tabla Detalle Ingreso
        public ICollection<DetalleIngreso> ingresos { get; set; }



        //referencia de la tabla Usuario
        [Required]
        [ForeignKey("IdUsuario")]
        public Usuario usuario { get; set; }


        //referencia de la tabla Persona
        [Required]
        [ForeignKey("IdProveedor")]
        public Persona persona { get; set; }

        //referencia de la tabla Comprobante
        [Required]
        [ForeignKey("IdComprobante")]
        public Comprobante comprobante { get; set; }

        //referencia de la tabla Caja
        [Required]
        [ForeignKey("IdCaja")]
        public Caja caja { get; set; }

    }
}
