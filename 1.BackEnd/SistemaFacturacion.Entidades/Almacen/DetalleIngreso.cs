using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class DetalleIngreso
    {
        [Key]
        public int IdDetalleIngreso { get; set; }
      
        public int IdIngreso { get; set; }   
        public int IdArticulo { get; set; }
        

        public int Cantidad { get; set; }      
        public decimal Precio { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }


        //referencia de la tabla Ingreso
        [Required]
        [ForeignKey("IdIngreso")]
        public Ingreso ingreso { get; set; }

        //referencia de la tabla Articulo
        [Required]
        [ForeignKey("IdArticulo")]
        public Articulo articulo { get; set; }

    }
}
