namespace SistemaFacturacion.Web.Models.Almacen.CategoriaSecundaria
{
    public class CategoriaSecundariaViewModel
    {
        //entidades para restringir la entrada hacia la BD
        //validandos los servicios Web

        public int IdCategoriaSecundaria { get; set; }

        //valores de la categoria Principal
        public int IdCategoriaPrincipal { get; set; }
        public string CategoriaPrincipal { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
