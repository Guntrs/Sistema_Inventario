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
using SistemaFacturacion.Web.Models.Almacen.Comprobante;

namespace SistemaFacturacion.Web.Controllers
{

    [Authorize(Roles = "Almacen,Vendedor,Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComprobantesController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public ComprobantesController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR 
        // GET: api/Comprobantes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ComprobanteViewModel>> Listar()
        {
            var tipo = await _context.Comprobantes.Include(a => a.Impuesto).ToListAsync();

            //var articulo = await _context.Articulos.Include(a => a.categoria).ToListAsync();

            return tipo.Select(c => new ComprobanteViewModel
            {
                IdComprobante = c.IdComprobante,

                //impuestos
                IdImpuesto = c.IdImpuesto,
                Tasa = c.Impuesto.Tasa,
                TipoDocumento = c.Impuesto.TipoDocumento, 

                Descripcion = c.Descripcion,
                Serie = c.Serie,
                Correlativo = c.Correlativo,
                Condicion = c.Condicion
            });
        }
        //----------------------------------------------------------------------------------------


        // SELECCIONAR 
        // GET: api/Comprobantes/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {

            var tipo = await _context.Comprobantes

                .Where(c => c.Condicion == true)
                .Include(c => c.Impuesto)
                .ToListAsync();

            //   var tipo2 = await _context.KreaTipoComprobantes.Include(a => a.kreaimpuestos).ToListAsync();

            return tipo.Select(c => new SelectViewModel
            {
                IdComprobante = c.IdComprobante,
                Descripcion = c.Descripcion,
                Serie = c.Serie,
                Correlativo = c.Correlativo,

                IdImpuesto = c.IdImpuesto,
                Tasa = c.Impuesto.Tasa
                //  TipoImpuesto = c.TipoImpuesto
            });

        }
        //----------------------------------------------------------------------------------------

        //MOSTRAR 
        // GET: api/Comprobante/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var tipo = await _context.Comprobantes.FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(new ComprobanteViewModel
            {
                IdComprobante = tipo.IdComprobante,
                Descripcion = tipo.Descripcion,
                Serie = tipo.Serie,
                Correlativo = tipo.Correlativo,
                //  TipoImpuesto = tipo.TipoImpuesto,
                Condicion = tipo.Condicion
            });
        }
        //----------------------------------------------------------------------------------------

        //METODO 
        // PUT: api/Comprobante/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdComprobante <= 0)
            {
                return BadRequest();
            }

            var tipo = await _context.Comprobantes.FirstOrDefaultAsync(c => c.IdComprobante == model.IdComprobante);

            if (tipo == null)
            {
                return NotFound();
            }



            tipo.Descripcion = model.Descripcion;

            tipo.IdImpuesto = model.IdImpuesto;

            tipo.Serie = model.Serie;
            tipo.Correlativo = model.Correlativo;
            //     tipo.TipoImpuesto = model.TipoImpuesto;


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
        //----------------------------------------------------------------------------------------


        //METODO 
        // POST: api/Comprobante/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Comprobante tipo = new Comprobante
            {

                Descripcion = model.Descripcion,
                IdImpuesto = model.IdImpuesto,
                Serie = model.Serie,
                Correlativo = model.Correlativo,
                Condicion = true

            };

            _context.Comprobantes.Add(tipo);
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
        //---------------------------------------------------------------------------------

        //Mostrar Codigo
        // GET: api/Comprobantes/BuscarCodigo
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigo([FromRoute] int codigo)
        {

            var tipo = await _context.Comprobantes.Include(a => a.Impuesto)
                .Where(a => a.Condicion == true).
                SingleOrDefaultAsync(a => a.IdComprobante == codigo);

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(new ComprobanteViewModel
            {
                Descripcion = tipo.Descripcion,
                Serie = tipo.Serie,
                Correlativo = tipo.Correlativo,

                //Stock = articulo.Stock,
                //PrecioVenta = articulo.PrecioVenta,
                //Condicion = articulo.Condicion
            });
        }
        //---------------------------------------------------------------------------------

  

        //----------DESACTIVAR 
        // PUT: api/Comprobantes/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var tipo = await _context.Comprobantes.FirstOrDefaultAsync(c => c.IdComprobante == id);

            if (tipo == null)
            {
                return NotFound();
            }

            tipo.Condicion = false;

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
        //--------------------------------

        //----------ACTIVAR 
        // PUT: api/Comprobante/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)

        {


            if (id <= 0)
            {
                return BadRequest();
            }

            var tipo = await _context.Comprobantes.FirstOrDefaultAsync(c => c.IdComprobante == id);

            if (tipo == null)
            {
                return NotFound();
            }

            tipo.Condicion = true;

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



        private bool ComprobanteExists(int id)
        {
            return _context.Comprobantes.Any(e => e.IdComprobante == id);
        }
    }
}
