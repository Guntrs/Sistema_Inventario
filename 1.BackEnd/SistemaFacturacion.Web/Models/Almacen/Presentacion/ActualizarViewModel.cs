using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Presentacion
{
    public class ActualizarViewModel
    {
        public int IdPresentacion { get; set; }
        public int Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }

        
    }
}
