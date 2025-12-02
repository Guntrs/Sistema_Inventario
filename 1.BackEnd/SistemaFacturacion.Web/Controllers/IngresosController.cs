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
using SistemaFacturacion.Web.Models.Almacen.Ingreso;

namespace SistemaFacturacion.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;
        public IngresosController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //CONSULTAR VENTAS
        // GET: api/Ventas/ConsultaFechas/FechaInicio/FechaFin
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<IngresoViewModel>> ConsultaFechas([FromRoute] DateTime FechaInicio, DateTime FechaFin)
        {

            var venta = await _context.Ingresos
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(i => i.FechaHora >= FechaInicio)
                .Where(i => i.FechaHora <= FechaFin)
                .Include(v => v.comprobante)
                .Include(v => v.caja)

                .OrderByDescending(v => v.IdIngreso)
                .Take(300)
                .ToListAsync();

            return venta.Select(v => new IngresoViewModel
            {
                IdIngreso = v.IdIngreso,

                IdProveedor = v.IdProveedor,
                Proveedor = v.persona.Nombre,

                IdUsuario = v.IdUsuario,
                Usuario = v.usuario.Nombre,

                IdCaja = v.IdCaja,
                DescripcionCaja = v.caja.Descripcion,

                IdComprobante = v.IdComprobante,
                Descripcion = v.comprobante.Descripcion,
                Serie = v.comprobante.Serie,
                Correlativo = v.comprobante.Correlativo,

                TipoComprobante = v.TipoComprobante,
                SerieComprobante = v.SerieComprobante,
                NumeroComprobante = v.NumeroComprobante,

                FechaHora = v.FechaHora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }

        //-------------------------------------------------------------------


        // GET: api/Ingresos/Listar/texto
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<IngresoViewModel>> Listar()
        {
            var ingreso = await _context.Ingresos
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .Include(i => i.caja)
                .Include(i => i.comprobante)
                .OrderByDescending(i => i.IdIngreso)
                .Take(100)  //cantidad 100 ingresos
                .ToListAsync();

            return ingreso.Select(i => new IngresoViewModel
            {
                IdIngreso = i.IdIngreso,

                IdProveedor = i.IdProveedor,
                Proveedor = i.persona.Nombre,

                IdUsuario = i.IdUsuario,
                Usuario = i.usuario.Nombre,

                IdCaja = i.IdCaja,
                DescripcionCaja = i.caja.Descripcion,

                IdComprobante = i.IdComprobante,
                Descripcion = i.comprobante.Descripcion,
                Serie = i.comprobante.Serie,
                Correlativo = i.comprobante.Correlativo,

                TipoComprobante = i.TipoComprobante,
                SerieComprobante = i.SerieComprobante,
                NumeroComprobante = i.NumeroComprobante,
                FechaHora = i.FechaHora,
                Impuesto = i.Impuesto,
                Total = i.Total,
                Estado = i.Estado
            });
        }
        //-----------------------------------------------------

        // busqueda mas de 100 ingresos
        // GET: api/Ingresos/Listar/texto
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<IngresoViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var ingreso = await _context.Ingresos
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .Include(i => i.caja)
                .Include(i => i.comprobante)
                .Where(i => i.SerieComprobante.Contains(texto))
                .OrderByDescending(i => i.IdIngreso)
                .Take(100)
                .ToListAsync();

            return ingreso.Select(i => new IngresoViewModel
            {
                IdIngreso = i.IdIngreso,

                IdProveedor = i.IdProveedor,
                Proveedor = i.persona.Nombre,

                IdUsuario = i.IdUsuario,
                Usuario = i.usuario.Nombre,

                IdCaja = i.IdCaja,
                DescripcionCaja = i.caja.Descripcion,

                IdComprobante = i.IdComprobante,
                Descripcion = i.comprobante.Descripcion,
                Serie = i.comprobante.Serie,
                Correlativo = i.comprobante.Correlativo,



                TipoComprobante = i.TipoComprobante,
                SerieComprobante = i.SerieComprobante,
                NumeroComprobante = i.NumeroComprobante,
                FechaHora = i.FechaHora,
                Impuesto = i.Impuesto,
                Total = i.Total,
                Estado = i.Estado
            });

        }

        //------------------------------------------------------------------------------------


        // GET: api/Ingresos/ListarDetalles/1
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]/{idingreso}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idingreso)
        {
            var detalle = await _context.DetallesIngresos
                .Include(a => a.articulo)
                .Where(d => d.IdIngreso == idingreso)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {

                IdArticulo = d.IdArticulo,
                Articulo = d.articulo.Nombre,
                Codigo = d.articulo.Codigo,

                Descripcion = d.articulo.Descripcion,
                Cantidad = d.Cantidad,
                Precio = d.Precio
            });

        }
        //------------------------------------------------------------------------------------




        //***************************************************************************
        // POST: api/Ingresos/Crear

        [Authorize(Roles = "Almacen,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;


            Ingreso ingreso = new Ingreso
            {
                IdProveedor = model.IdProveedor,
                IdUsuario = model.IdUsuario,
                IdCaja   = model.IdCaja,
                IdComprobante = model.IdComprobante,

                TipoComprobante = model.TipoComprobante,
                SerieComprobante = model.SerieComprobante,
                NumeroComprobante = model.NumeroComprobante,
                FechaHora = fechaHora,
                Impuesto = model.Impuesto,
                Total = model.Total,
                Estado = "Aceptado"
            };
            try
            {
                _context.Ingresos.Add(ingreso);
                await _context.SaveChangesAsync();

                var id = ingreso.IdIngreso;
                foreach (var det in model.detalles)
                {
                    DetalleIngreso detalle = new DetalleIngreso
                    {
                        IdIngreso = id,
                        IdArticulo = det.IdArticulo,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio
                    };
                    _context.DetallesIngresos.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        //-----------------------------------------------------------------------------------


        // PUT: api/Ingresos/Anular/1
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var ingreso = await _context.Ingresos.FirstOrDefaultAsync(i => i.IdIngreso == id);

            if (ingreso == null)
            {
                return NotFound();
            }

            ingreso.Estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();
                // Inicio de código para devolver stock
                // 1. Obtenemos los detalles
                var detalle = await _context.DetallesIngresos.Include(a => a.articulo).Where(d => d.IdIngreso == id).ToListAsync();
                //2. Recorremos los detalles
                foreach (var det in detalle)
                {
                    //Obtenemos el artículo del detalle actual
                    var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == det.articulo.IdArticulo);
                    //actualizamos el stock
                    articulo.Stock = det.articulo.Stock - det.Cantidad;
                    //Guardamos los cambios
                    await _context.SaveChangesAsync();
                }
                // Fin del código para devolver stock
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }
        //-----------------------------------------------------------------------------------


        private bool IngresoExists(int id)
        {
            return _context.Ingresos.Any(e => e.IdIngreso == id);
        }
    }
}
