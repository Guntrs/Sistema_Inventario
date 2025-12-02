using SistemaFacturacion.Entidades.Almacen;
using SistemaFacturacion.Entidades.Factura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaFacturacion.Entidades.Usuarios
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        public int IdRol { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Required]
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        public bool Condicion { get; set; }


        //referencia de la tabla Categoria
        [Required]
        [ForeignKey("IdRol")]
        public Rol rol { get; set; }

        //para la tabla  Ingreso
        public ICollection<Ingreso> ingresos { get; set; }

        //para la tabla  CertificadoDigital
        public ICollection<CertificadoDigital> certificados { get; set; }

        ////para la tabla Ventas
        //  public ICollection<KreaVenta> kreaventas { get; set; }
    }
}
