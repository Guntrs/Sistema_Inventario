using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Usuarios.Rol
{
    public class ActualizarViewModel
    {
        
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
