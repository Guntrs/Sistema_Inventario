using System.ComponentModel.DataAnnotations;

namespace SistemaFacturacion.Web.Models.Sucursal.Sucursal
{
    public class CrearViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
