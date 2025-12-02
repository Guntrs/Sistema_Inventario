using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Ventas.Persona
{
    public class SelectViewModel
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }

        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }

        public string CodigoPersona { get; set; }


    }
}
