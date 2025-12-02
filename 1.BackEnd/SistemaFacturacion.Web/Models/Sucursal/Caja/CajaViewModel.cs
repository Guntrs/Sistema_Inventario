namespace SistemaFacturacion.Web.Models.Sucursal.Caja
{
    public class CajaViewModel
    {
        public int IdCaja { get; set; }

        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }

       
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
