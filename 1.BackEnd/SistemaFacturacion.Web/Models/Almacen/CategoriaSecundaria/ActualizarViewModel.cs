using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Almacen.CategoriaSecundaria
{
    public class ActualizarViewModel
    {
      
        public int IdCategoriaSecundaria { get; set; }

        public int IdCategoriaPrincipal { get; set; }


        
        public string Nombre { get; set; }  
        public string Descripcion { get; set; }
    }
}
