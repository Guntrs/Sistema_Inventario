using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Categoria
{
    public class CrearViewModel
    {
        public int IdCategoriaSecundaria { get; set; }
        public string Nombre { get; set; }
   
        public string Descripcion { get; set; }

    }
}
