using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Ventas;
using SistemaFacturacion.Web.Models.Ventas.Venta;

namespace SistemaFacturacion.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public VentasController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //*************************************************************

        //LISTAR LA VENTA
        // GET: api/Ventas/Listar
       // [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<VentaViewModel>> Listar()
        {
            var venta = await _context.Ventas
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Include(v => v.comprobante)
                .Include(v => v.caja)

                .OrderByDescending(v => v.IdVenta)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                IdVenta = v.IdVenta,

                IdCliente = v.IdCliente,
                Cliente = v.persona.Nombre,
                Documento = v.persona.NumeroDocumento,
                Direccion = v.persona.Direccion,

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

        //-----------------------------------------------------------------------------------------------------

        //LISTAR LA VENTA
        // GET: api/Ventas/VentasMes12
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConsultaViewModel>>VentasMes12()
        {
            var consulta = await _context.Ventas
                .GroupBy(v => v.FechaHora.Month)
                .Select(x => new { etiqueta = x.Key, valor = x.Sum(v => v.Total) })
                .OrderByDescending(x => x.etiqueta)
                .Take(12)
                .ToListAsync();

            return consulta.Select(v => new ConsultaViewModel
            {
                etiqueta = v.etiqueta.ToString(),
                valor = v.valor
            });

        }

        //-----------------------------------------------------------------------------------------------------


        // GET: api/Ventas/ListarFiltro/texto
         [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<VentaViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var venta = await _context.Ventas
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(v => v.SerieComprobante.Contains(texto))
                .OrderByDescending(v => v.IdVenta)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                IdVenta = v.IdVenta,
                IdCliente = v.IdCliente,
                Cliente = v.persona.Nombre,
                IdUsuario = v.IdUsuario,
                Usuario = v.usuario.Nombre,
                TipoComprobante = v.TipoComprobante,
                SerieComprobante = v.SerieComprobante,
                NumeroComprobante = v.NumeroComprobante,
                FechaHora = v.FechaHora,
                Impuesto = v.Impuesto,
                Total = v.Total,
                Estado = v.Estado
            });

        }
        //--------------------------------------------------------------------------------------------------------

        //CONSULTAR VENTAS
        // GET: api/Ventas/ConsultaFechas/FechaInicio/FechaFin
         [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{FechaInicio}/{FechaFin}")]
        public async Task<IEnumerable<VentaViewModel>> ConsultaFechas([FromRoute] DateTime FechaInicio, DateTime FechaFin)
        {
            var venta = await _context.Ventas
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(i => i.FechaHora >= FechaInicio)
                .Where(i => i.FechaHora <= FechaFin)
                .Include(v => v.comprobante)
                .Include(v => v.caja)

                .OrderByDescending(v => v.IdVenta)
                .Take(300)
                .ToListAsync();

            return venta.Select(v => new VentaViewModel
            {
                IdVenta = v.IdVenta,

                IdCliente = v.IdCliente,
                Cliente = v.persona.Nombre,
                Documento = v.persona.NumeroDocumento,
                Direccion = v.persona.Direccion,

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

        // GET: api/Ventas/ListarDetalles
     //   [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{idventa}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idventa)
        {
            var detalle = await _context.DetallesVentas //incluir el id de la Venta encabezado
                .Include(a => a.articulo)
                .Where(d => d.IdVenta == idventa)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {
                IdArticulo = d.IdArticulo,
                Articulo = d.articulo.Nombre,
                Codigo = d.articulo.Codigo,
                Descripcion = d.articulo.Descripcion,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Descuento = d.Descuento
            });

        }
        //----------------------------------


        // ---------------------CREAR  VENTAS-------------------------------------------------------------------
        // POST: api/Ventas/Crear
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;

            //-------------------------------------------------------------------------
            //var tipo = model.CodigoComprobante; //id tipo comprobante

            //if (await _context.KreaPersonas.AnyAsync(p => p.Email == email))
            //{
            //    return BadRequest("El email ya existe");
            //}

            //-----------------------------------------------------------------------------
            //cabecera de la factura
            Venta venta = new Venta
            {
                IdCliente = model.IdCliente,
                IdUsuario = model.IdUsuario,       
                IdCaja = model.IdCaja,
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
                // detalles de la factura
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                var id = venta.IdVenta; //id del valor de la venta

                foreach (var det in model.detalles)
                {
                    DetalleVenta detalle = new DetalleVenta //instancia a la clase venta
                    {
                        IdVenta = id,
                        IdArticulo = det.IdArticulo,
                        Cantidad = det.Cantidad,
                        Precio = det.Precio,
                        Descuento = det.Descuento
                    };

                    _context.DetallesVentas.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        //---------------------------------------------------------------------------------------------------



        // PUT: api/Ventas/Anular/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.IdVenta == id);

            if (venta == null)
            {
                return NotFound();
            }

            venta.Estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();
                // Inicio de código para devolver stock
                // 1. Obtenemos los detalles
                var detalle = await _context.DetallesVentas.Include(a => a.articulo).Where(d => d.IdVenta == id).ToListAsync();
                //2. Recorremos los detalles
                foreach (var det in detalle)
                {
                    //Obtenemos el artículo del detalle actual
                    var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.IdArticulo == det.articulo.IdArticulo);
                    //actualizamos el stock
                    articulo.Stock = det.articulo.Stock + det.Cantidad; ///pendiente 
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
        //***************************************************************





        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.IdVenta == id);
        }
    }
}
