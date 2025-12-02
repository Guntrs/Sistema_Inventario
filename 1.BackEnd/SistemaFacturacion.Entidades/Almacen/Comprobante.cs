using SistemaFacturacion.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Comprobante
    {
        [Key]
        public int IdComprobante { get; set; }
        public int IdImpuesto { get; set; }
        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public int Correlativo { get; set; }
        public bool Condicion { get; set; }




        //referencia de la tabla Impuesto
        [Required]
        [ForeignKey("IdImpuesto")]
        public Impuesto Impuesto { get; set; }

        //coleccion para la tabla Ingreso
        public ICollection<Ingreso> ingresosc { get; set; }

        //coleccion para la tabla Venta
        public ICollection<Venta> ventass { get; set; }
    }
}
