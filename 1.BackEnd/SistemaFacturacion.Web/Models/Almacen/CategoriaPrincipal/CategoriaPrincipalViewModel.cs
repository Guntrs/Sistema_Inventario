namespace SistemaFacturacion.Web.Models.Almacen.CategoriaPrincipal
{
    public class CategoriaPrincipalViewModel
    {
        //entidades para restringir la entrada hacia la BD
        //validandos los servicios Web

        public int IdCategoriaPrincipal { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
