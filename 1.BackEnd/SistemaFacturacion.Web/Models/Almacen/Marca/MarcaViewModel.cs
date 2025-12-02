using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Web.Models.Almacen.Marca
{
    public class MarcaViewModel
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool Condicion { get; set; }
    }
}
