using SistemaFacturacion.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Sucursal
{
    public class Caja
    {
        [Key]
        public int IdCaja { get; set; }
        public int IdSucursal { get; set; }
        
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }



        //referencia de la tabla Categoria
        [Required]
        [ForeignKey("IdSucursal")]
        public Sucursal1 Sucursal { get; set; }


        //coleccion para la tabla Venta
        public ICollection<Ingreso> ingresosc { get; set; }

    }
}
