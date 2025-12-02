using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Almacen.Ingreso
{
    public class DetalleViewModel
    {
        [Required]
        public int IdArticulo { get; set; }


        public string Articulo { get; set; }
        public string Codigo { get; set; }

        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

    }
}
