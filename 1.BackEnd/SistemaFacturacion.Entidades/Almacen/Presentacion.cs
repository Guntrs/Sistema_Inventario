using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Presentacion
    {
        public int IdPresentacion { get; set; }
        public int Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }


    }
}
