using SistemaFacturacion.Entidades.Sucursal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaFacturacion.Entidades.Almacen
{
     public class CategoriaSecundaria
    {
        [Key]
        public int IdCategoriaSecundaria { get; set; }
        public int IdCategoriaPrincipal { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        //referencia de la tabla CategoriaPrincipal
        [Required]
        [ForeignKey("IdCategoriaPrincipal")]
        public CategoriaPrincipal CategoriaPrincipal { get; set; }


        //coleccion para la tabla Categoria
        //  public ICollection<Ingreso> ingresosc { get; set; }

         public ICollection<Categoria> categoriasje { get; set; }


    }
}
