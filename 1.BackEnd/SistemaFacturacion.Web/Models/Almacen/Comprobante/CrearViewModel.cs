using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Comprobante
{
    public class CrearViewModel
    {
        public int IdImpuesto { get; set; }
        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public int Correlativo { get; set; }
        public bool Condicion { get; set; }
    }
}
