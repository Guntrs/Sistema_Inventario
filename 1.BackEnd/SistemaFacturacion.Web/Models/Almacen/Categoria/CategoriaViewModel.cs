using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Categoria
{
    public class CategoriaViewModel
    {
        //entidades para restringir la entrada hacia la BD
        //validandos los servicios Web

        public int IdCategoria { get; set; }

        public int IdCategoriaSecundaria { get; set; }
        public string CategoriaSecundaria { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
