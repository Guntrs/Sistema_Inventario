using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Ventas.Venta
{
    public class DetalleViewModel
    {
      
        public int IdArticulo { get; set; }

        public string Articulo { get; set; }
        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public int Cantidad { get; set; }     
        public decimal Precio { get; set; }     
        public decimal Descuento { get; set; }
    }
}
