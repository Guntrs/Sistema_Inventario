using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaFacturacion.Entidades.Sucursal
{
     public class Sucursal1
    {
        [Key]
        public int IdSucursal { get; set; }    
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Condicion { get; set; }


     

        //para la tabla Caja (cajas)
        public ICollection<Caja> cajas { get; set; }

    }
}
