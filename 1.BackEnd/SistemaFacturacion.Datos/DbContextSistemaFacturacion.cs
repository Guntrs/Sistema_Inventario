using SistemaFacturacion.Datos.Mapping.Almacen;
using SistemaFacturacion.Entidades.Almacen;


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaFacturacion.Entidades.Usuarios;
using SistemaFacturacion.Datos.Mapping.Usuarios;
using SistemaFacturacion.Entidades.Ventas;
using SistemaFacturacion.Datos.Mapping.Ventas;
using SistemaFacturacion.Entidades.Sucursal;
using SistemaFacturacion.Datos.Mapping.Sucursal;
using SistemaFacturacion.Entidades.Factura;
using SistemaFacturacion.Datos.Mapping.Factura;

namespace SistemaFacturacion.Datos
{
    public class DbContextSistemaFacturacion : DbContext
    {
        //COLECCION 

        //Almacen---------------------------------------------------------------
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaSecundaria> CategoriaSecundarias { get; set; }
        public DbSet<CategoriaPrincipal> CategoriaPrincipales { get; set; }


        public DbSet<Impuesto> Impuestos { get; set; }
        public DbSet<Presentacion> Presentaciones { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }




        //Ingreso ------------------------------------------------------------- 
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<DetalleIngreso> DetallesIngresos { get; set; }

        //Ventas ------------------------------------------------------------- 

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVentas { get; set; }


        public DbSet<Sucursal1> Sucursales { get; set; }
        public DbSet<Caja> Cajas { get; set; }

        //------------------------------------------------------------- 

        //Usuarios---------------------------------------------------------------
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //-------------------------------------------------------------

        //Personas---------------------------------------------------------------
        public DbSet<Persona> Personas { get; set; }
        //-------------------------------------------------------------

       
        //FACTURA ---------------------------------------------------------------
        public DbSet<BitacoraEnvio> Bitacoras { get; set; }
        //-------------------------------------------------------------
        public DbSet<CertificadoDigital> Certificados { get; set; }
        //-------------------------------------------------------------


        //CONSTRUCTOR
        public DbContextSistemaFacturacion(DbContextOptions<DbContextSistemaFacturacion> options) : base(options)
        {

        }

        //METODO PARA MAPEAR ENTIDADES
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Almacen
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CategoriaSecundariaMap());
            modelBuilder.ApplyConfiguration(new CategoriaPrincipalMap());

            modelBuilder.ApplyConfiguration(new ImpuestoMap());
            modelBuilder.ApplyConfiguration(new PresentacionMap());
            modelBuilder.ApplyConfiguration(new MarcaMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new ComprobanteMap());

            ////ingresos
            modelBuilder.ApplyConfiguration(new IngresoMap());
            modelBuilder.ApplyConfiguration(new DetalleIngresoMap());
            //-------------------------------------------------------

            ////Ventas
            modelBuilder.ApplyConfiguration(new VentaMap());
            modelBuilder.ApplyConfiguration(new DetalleVentaMap());
            //-------------------------------------------------------

            modelBuilder.ApplyConfiguration(new CajaMap());
            modelBuilder.ApplyConfiguration(new SucursalMap());
            //-------------------------------------------------------

            //Usuarios----------------------------------------------
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            //-------------------------------------------------------
            modelBuilder.ApplyConfiguration(new PersonaMap());

            //Factura-------------------------------------------------------
            modelBuilder.ApplyConfiguration(new BitacoraEnvioMap());
            modelBuilder.ApplyConfiguration(new CertificadoDigitalMap());


        }

    }
}
