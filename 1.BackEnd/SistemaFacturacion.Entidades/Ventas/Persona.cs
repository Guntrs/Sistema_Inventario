using SistemaFacturacion.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaFacturacion.Entidades.Ventas
{
    public class Persona
    {
        public int IdPersona { get; set; }
        [Required]
        public string TipoPersona { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public string CodigoPersona { get; set; }



        //para la tabla  Ingreso
        public ICollection<Ingreso> ingresos { get; set; }

        //para la tabla Ventas
        public ICollection<Venta> ventas { get; set; }


    }
}
