
using SistemaFacturacion.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }
        public int IdCategoria { get; set; }
        public int IdImpuesto { get; set; }
        public int IdMarca { get; set; }


        public string Codigo { get; set; }       
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }

        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal Utilidad { get; set; }



        public int Stock { get; set; }       
        public bool Condicion { get; set; }
        public bool Inventariable { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }






        //referencia de la tabla Categoria
        [Required]
        [ForeignKey("IdCategoria")]     
        public Categoria Categoria { get; set; }

        //referencia de la tabla Impuesto
        [Required]
        [ForeignKey("IdImpuesto")]
        public Impuesto Impuesto { get; set; }

        //referencia de la tabla Impuesto
        [Required]
        [ForeignKey("IdMarca")]
        public Marca Marca { get; set; }


        //referencia de la tabla sucursal
        //public KreaSucursal kreasucursal { get; set; }

        ////para la tabla Detalle Ingreso
         public ICollection<DetalleIngreso> DetallesIngresos { get; set; }

        //para la tabla Detalle Venta
         public ICollection<DetalleVenta> DetallesVentas { get; set; }




    }
}
