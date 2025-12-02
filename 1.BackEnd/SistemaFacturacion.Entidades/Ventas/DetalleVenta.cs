using SistemaFacturacion.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Ventas
{
    public class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }
       
        public int IdVenta { get; set; }
       
        public int IdArticulo { get; set; }
        
        public int Cantidad { get; set; }
        
        public decimal Precio { get; set; }
       
        public decimal Descuento { get; set; }


        //referencia de la tabla Venta
        [Required]
        [ForeignKey("IdVenta")]
        public Venta venta { get; set; }

        //referencia de la tabla Articulo
        [Required]
        [ForeignKey("IdArticulo")]
        public Articulo articulo { get; set; }
    }
}
