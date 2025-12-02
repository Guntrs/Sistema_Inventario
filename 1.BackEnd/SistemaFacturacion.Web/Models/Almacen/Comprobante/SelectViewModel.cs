using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Comprobante
{
    public class SelectViewModel
    {
        public int IdComprobante { get; set; }

        public int IdImpuesto { get; set; }
        public decimal Tasa { get; set; }  //tasa impuesto elegido

        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public int Correlativo { get; set; }
    }
}
