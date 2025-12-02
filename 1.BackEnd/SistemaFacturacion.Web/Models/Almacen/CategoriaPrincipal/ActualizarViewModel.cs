using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Almacen.CategoriaPrincipal
{
    public class ActualizarViewModel
    {
        [Required]
        public int IdCategoriaPrincipal { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
    }
}
