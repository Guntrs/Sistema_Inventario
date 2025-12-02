using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Sucursal.Caja
{
    public class CrearViewModel
    {
        public int IdSucursal { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
      
        public string Descripcion { get; set; }
    }
}
