using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Marca
    {

        public int IdMarca { get; set; }
        public string Nombre{ get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        //para la tabla Articulos 
        public ICollection<Articulo> marca { get; set; }

    }
}
