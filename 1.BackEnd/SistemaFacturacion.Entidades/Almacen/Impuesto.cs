
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Impuesto
    {
        [Key]
        public int IdImpuesto { get; set; }       
        public string TipoDocumento { get; set; }

        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public decimal Tasa { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }


        //para la tabla Articulos 
        public ICollection<Articulo> impuesto { get; set; }

        //para la tabla Comprobante
        public ICollection<Comprobante> impuestocom { get; set; }


    }
}
