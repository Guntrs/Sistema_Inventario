using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Impuesto
{
    public class SelectViewModel
    {
        public int IdImpuesto { get; set; }
        public string TipoDocumento { get; set; }

        public string Descripcion { get; set; }
        public decimal Tasa { get; set; }
    }
}
