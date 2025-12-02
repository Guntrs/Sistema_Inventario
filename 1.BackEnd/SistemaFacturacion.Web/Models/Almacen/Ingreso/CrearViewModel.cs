using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Almacen.Ingreso
{
    public class CrearViewModel
    {
        //Propiedades maestro
        
        public int IdProveedor { get; set; }       
        public int IdUsuario { get; set; }
        public int IdCaja { get; set; }
        public int IdComprobante { get; set; }   

        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }     
        public int NumeroComprobante { get; set; }    
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }


        //Propiedades detalle
        [Required]
        public List<DetalleViewModel> detalles { get; set; }
    }
}
