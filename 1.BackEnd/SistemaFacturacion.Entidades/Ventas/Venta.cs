using SistemaFacturacion.Entidades.Almacen;
using SistemaFacturacion.Entidades.Factura;
using SistemaFacturacion.Entidades.Sucursal;
using SistemaFacturacion.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Ventas
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }  

        public int IdCliente { get; set; }    
        public int IdUsuario { get; set; }
        public int IdComprobante { get; set; }
        public int IdCaja { get; set; }

        //Comprobante

        //datos del TipoComprobante
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public int NumeroComprobante { get; set; }

        public DateTime FechaHora { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }

     
        public Guid UUID { get; set; } = Guid.NewGuid();
        public string XMLFactura { get; set; }
        public string SerieSAT { get; set; }

        public int? NumeroSAT { get; set; }



        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }




        //para la tabla DetalleVenta
        public ICollection<DetalleVenta> detalleventa { get; set; }

        //para la tabla Bitacora
        public ICollection<BitacoraEnvio> detalleventab { get; set; }


        //referencia de la tabla Usuario
        [Required]
        [ForeignKey("IdUsuario")]
        public Usuario usuario { get; set; }


        //referencia de la tabla Persona
        [Required]
        [ForeignKey("IdCliente")]
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
