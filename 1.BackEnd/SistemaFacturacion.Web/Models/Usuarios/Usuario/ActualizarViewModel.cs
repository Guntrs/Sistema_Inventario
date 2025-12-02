using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Usuarios.Usuario
{
    public class ActualizarViewModel
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdRol { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
        public bool act_password { get; set; }
    }
}
