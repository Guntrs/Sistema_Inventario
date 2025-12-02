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


using SistemaFacturacion.Web.Models.Almacen.Impuesto;


namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Almacen,Administrador")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class ImpuestosController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public ImpuestosController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }




        //LISTAR Impuesto
        // GET: api/Impuestos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ImpuestoViewModel>> Listar()
        {
            var impuesto = await _context.Impuestos.ToListAsync();

            return impuesto.Select(c => new ImpuestoViewModel
            {
                IdImpuesto = c.IdImpuesto,
                TipoDocumento = c.TipoDocumento,
                Descripcion = c.Descripcion,
                Tasa = c.Tasa
            });

        }
        //------------------------------------------------


        // SELECCIONAR Impuesto
        // GET: api/Impuestos/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var impuesto = await _context.Impuestos.ToListAsync();

            return impuesto.Select(c => new SelectViewModel
            {
                IdImpuesto = c.IdImpuesto,
                Tasa = c.Tasa,
                Descripcion = c.Descripcion,
                TipoDocumento = c.TipoDocumento

            });

        }


        //MOSTRAR Impuesto
        // GET: api/Impuesto/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var impuesto = await _context.Impuestos.FindAsync(id);

            if (impuesto == null)
            {
                return NotFound();
            }

            return Ok(new ImpuestoViewModel
            {
                IdImpuesto = impuesto.IdImpuesto,
                TipoDocumento = impuesto.TipoDocumento,
                Descripcion = impuesto.Descripcion,
                Tasa = impuesto.Tasa
            });
        }

        //METODO ACTUALIZAR Impuesto
        // PUT: api/Impuestos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdImpuesto <= 0)
            {
                return BadRequest();
            }

            var impuesto = await _context.Impuestos.FirstOrDefaultAsync(c => c.IdImpuesto == model.IdImpuesto);

            if (impuesto == null)
            {
                return NotFound();
            }

            impuesto.TipoDocumento = model.TipoDocumento;
            impuesto.Descripcion = model.Descripcion;
            impuesto.Tasa = model.Tasa;

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
        //-------------------------------------------------------------------------------------


        //METODO CREAR  Impuesto
        // POST: api/Impuestos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Impuesto impuesto = new Impuesto
            {
                TipoDocumento = model.TipoDocumento,
                Descripcion = model.Descripcion,
                Tasa = model.Tasa,
                Condicion = true
            };

            _context.Impuestos.Add(impuesto);
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

        //--------ELIMINAR Impuesto
        // DELETE: api/Impuestos/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Impuestos.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Impuestos.Remove(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(categoria);
        }
        //--------------------------------  



        private bool ImpuestoExists(int id)
        {
            return _context.Impuestos.Any(e => e.IdImpuesto == id);
        }
    }
}
