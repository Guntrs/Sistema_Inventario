using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Almacen.Impuesto
{
    public class CrearViewModel
    {


        
        public string TipoDocumento { get; set; }
        
        public string Descripcion { get; set; }

        public decimal Tasa { get; set; }
    }
}
