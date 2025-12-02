using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Categoria
{
    public class ActualizarViewModel
    {

        [Required]
        public int IdCategoria { get; set; }

        public int IdCategoriaSecundaria { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
    }
}
