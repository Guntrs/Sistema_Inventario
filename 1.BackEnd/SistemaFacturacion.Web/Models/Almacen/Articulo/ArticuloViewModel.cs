using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Articulo
{
    public class ArticuloViewModel
    {
        public int IdArticulo { get; set; }


        //categoria
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }

        //impuesto
        public int IdImpuesto { get; set; }
        public decimal Tasa { get; set; }
        public string TipoDocumento { get; set; }
        
        //Marca
        public int IdMarca { get; set; }
        public string Marca { get; set; }


        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }


        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal Utilidad { get; set; }
        public int Stock { get; set; }      
        public bool Condicion { get; set; }
        public bool Inventariable { get; set; }

        public DateTime FechaVencimiento { get; set; }
    }
}
