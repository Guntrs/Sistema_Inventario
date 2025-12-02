using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Articulo
{
    public class ActualizarViewModel
    {
        
        public int IdArticulo { get; set; }       
        public int IdCategoria { get; set; }
        public int IdImpuesto { get; set; }
        public int IdMarca { get; set; }


        public string Codigo { get; set; }        
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal Utilidad { get; set; }

        public int Stock { get; set; }

        public DateTime FechaVencimiento { get; set; }
    }
}
