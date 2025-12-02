using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Ventas.Persona
{
    public class CrearViewModel
    {
        
        public string TipoPersona { get; set; }
        
        public string Nombre { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public string CodigoPersona { get; set; }
    }
}
