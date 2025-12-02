using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Almacen;
using SistemaFacturacion.Web.Models.Almacen.Articulo;

namespace SistemaFacturacion.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public ArticulosController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }



        //-------------------------------------------------------------------------------------------------
        // GET: api/Articulos/Listar
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Impuesto)
                .Include(a => a.Marca)
                .OrderByDescending(a => a.IdArticulo)
                .Take(100)  //cantidad 100 ingresos
                .ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {

                IdArticulo = a.IdArticulo,

                //Categoria
                IdCategoria = a.IdCategoria,
                Categoria = a.Categoria.Nombre,

                //impuestos
                IdImpuesto = a.IdImpuesto,
                Tasa = a.Impuesto.Tasa,
                TipoDocumento = a.Impuesto.TipoDocumento,

                //Marca
                IdMarca = a.IdMarca,
                Marca = a.Marca.Nombre,

                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Cantidad = a.Cantidad,
                Unidad = a.Unidad,
                Descripcion = a.Descripcion,

                Stock = a.Stock,
                PrecioVenta = a.PrecioVenta,
                PrecioCompra = a.PrecioCompra,
                Utilidad = a.Utilidad,
                Condicion = a.Condicion,
                Inventariable = a.Inventariable,

                FechaVencimiento = a.FechaVencimiento,
            });

        }
        //---------------------------------------------------------------------------------------------------

        //------------------------------------------Actualizar---------------------------------------------------------
        // PUT: api/Articulos/Actualizar
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdArticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == model.IdArticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.IdCategoria = model.IdCategoria;
            articulo.IdImpuesto = model.IdImpuesto;
            articulo.IdMarca = model.IdMarca;
            articulo.Codigo = model.Codigo;
            articulo.Nombre = model.Nombre;
            articulo.Cantidad = model.Cantidad;
            articulo.Unidad = model.Unidad;
            articulo.Descripcion = model.Descripcion;
            articulo.PrecioVenta = model.PrecioVenta;
            articulo.PrecioCompra = model.PrecioCompra;
            articulo.Utilidad = model.Utilidad;
            articulo.Stock = model.Stock;

            articulo.FechaVencimiento = model.FechaVencimiento;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        //---------------------------------------------------------------------------------------------------


        //-----------------------------------------Crear----------------------------------------------------------
        // POST: api/Articulos/Crear
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Articulo articulo = new Articulo
            {
                IdCategoria = model.IdCategoria,
                IdImpuesto = model.IdImpuesto,
                IdMarca = model.IdMarca,
                Codigo = model.Codigo,
                Nombre = model.Nombre,
                Cantidad = model.Cantidad,
                Unidad = model.Unidad,

                PrecioVenta = model.PrecioVenta,
                PrecioCompra = model.PrecioCompra,
                Utilidad = model.Utilidad,

                FechaVencimiento = model.FechaVencimiento,

                Stock = model.Stock,
                Descripcion = model.Descripcion,
                Inventariable = true,
                Condicion = true
            };

            _context.Articulos.Add(articulo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
        //---------------------------------------------------------------------------------------------------

        //-----------------------------------------Activar---------------------------------------------------
        // PUT: api/Articulos/Desactivar/1
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        //---------------------------------------------------------------------------------------------------

        //----------------------------------------Desactivar-------------------------------------------------
        // PUT: api/KreaArticulos/Activar/1
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.Condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        //---------------------------------------------------------------------------------------------------



        // ---------------------INGRESOS-------------------------------------------------------------------------
        // GET: api/Articulos/ListarIngreso/texto
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<ArticuloViewModel>> ListarIngreso([FromRoute] string texto)
        {
            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Marca)
                .Where(a => a.Nombre.Contains(texto))  // mostrar solomante que contiene el texto 
                .Where(a => a.Condicion == true)  //articulos activos
                .ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                IdArticulo = a.IdArticulo,

                IdCategoria = a.IdCategoria,
                Categoria = a.Categoria.Nombre,

                IdMarca = a.IdMarca,
                Marca = a.Marca.Nombre,



                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Stock = a.Stock,
                PrecioVenta = a.PrecioVenta,
                PrecioCompra = a.PrecioCompra,
                Utilidad = a.Utilidad,
                Descripcion = a.Descripcion,
                Condicion = a.Condicion
            });

        }


        //Mostrar Codigo Ingreso
        // GET: api/Articulos/BuscarCodigoIngreso/12345678
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoIngreso([FromRoute] string codigo)
        {

            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Marca)
                .Where(a => a.Condicion == true).
                SingleOrDefaultAsync(a => a.Codigo == codigo);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticuloViewModel
            {
                IdArticulo = articulo.IdArticulo,
                IdCategoria = articulo.IdCategoria,
                Categoria = articulo.Categoria.Nombre,
                Marca = articulo.Marca.Nombre,
                Codigo = articulo.Codigo,
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                Stock = articulo.Stock,
                PrecioVenta = articulo.PrecioVenta,
                PrecioCompra = articulo.PrecioCompra,
                Utilidad = articulo.Utilidad,
                Condicion = articulo.Condicion
            });
        }


        //----------------------VENTAS--------------------------------------------------------------------------------
        //Mostrar Codigo Ingreso
        // GET: api/Articulos/BuscarCodigoIngreso/12345678
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoVenta([FromRoute] string codigo)
        {

            var articulo = await _context.Articulos.Include(a => a.Categoria)
                .Where(a => a.Condicion == true)
                .Where(a => a.Stock > 0).
                SingleOrDefaultAsync(a => a.Codigo == codigo);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticuloViewModel
            {
                IdArticulo = articulo.IdArticulo,

                IdCategoria = articulo.IdCategoria,
                Categoria = articulo.Categoria.Nombre,

                //Marca
                IdMarca = articulo.IdMarca,
                Marca = articulo.Marca.Nombre,

                Codigo = articulo.Codigo,
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                Stock = articulo.Stock,
                PrecioVenta = articulo.PrecioVenta,
                Condicion = articulo.Condicion
            });
        }


        // GET: api/Articulos/ListarIngreso/texto
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<ArticuloViewModel>> ListarVenta([FromRoute] string texto)
        {
            var articulo = await _context.Articulos.Include(a => a.Categoria)
                .Where(a => a.Nombre.Contains(texto))  // mostrar solomante que contiene el texto 
                .Where(a => a.Condicion == true)  //articulos activos
                .Where(a => a.Stock > 0)
                .ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                IdArticulo = a.IdArticulo,
                IdCategoria = a.IdCategoria,
                Categoria = a.Categoria.Nombre,
                Codigo = a.Codigo,
                Nombre = a.Nombre,
                Stock = a.Stock,
                PrecioVenta = a.PrecioVenta,
                Descripcion = a.Descripcion,
                Condicion = a.Condicion
            });

        }



        //----------------------------------------------------------------------------------------------




        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.IdArticulo == id);
        }
    }
}
