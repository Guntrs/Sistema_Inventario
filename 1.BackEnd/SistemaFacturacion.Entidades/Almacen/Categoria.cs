using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
    public class Categoria
    {
            [Key]
            public int IdCategoria { get; set; }

            public int IdCategoriaSecundaria { get; set; }

            public string Nombre { get; set; }     
            public string Descripcion { get; set; }
            public bool Condicion { get; set; }

            public string UsuarioCrea { get; set; }
            public string UsuarioModifica { get; set; }
            public DateTime? FechaCreacion { get; set; }
            public DateTime? FechaModificacion { get; set; }

            //referencia de la tabla CategoriaSecundaria
            [Required]
            [ForeignKey("IdCategoriaSecundaria")]
            //  public Sucursal1 Sucursal { get; set; }
            public CategoriaSecundaria CategoriaSecundaria { get; set; }


        //para la tabla Articulos
        public ICollection<Articulo> categoria { get; set; }

    }
}
