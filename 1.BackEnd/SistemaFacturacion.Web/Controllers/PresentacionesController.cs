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
using SistemaFacturacion.Web.Models.Almacen.Presentacion;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Almacen,Administrador")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class PresentacionesController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public PresentacionesController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //--------------------------------------------------------------------------

        //LISTAR 
        // GET: api/Presentaciones/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PresentacionViewModel>> Listar()
        {
            var presentacion = await _context.Presentaciones.ToListAsync();

            return presentacion.Select(c => new PresentacionViewModel
            {
                IdPresentacion = c.IdPresentacion,
                Cantidad = c.Cantidad,
                Unidad = c.Unidad,
                Descripcion = c.Descripcion,
                Condicion = c.Condicion
            });

        }
        //----------------------------------------------------------------


        // SELECCIONAR
        // GET: api/Presentacion/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var presentacion = await _context.Presentaciones.Where(c => c.Condicion == true).ToListAsync();

            return presentacion.Select(c => new SelectViewModel
            {

                IdPresentacion = c.IdPresentacion,
                Cantidad = c.Cantidad,
                Unidad = c.Unidad,
                Descripcion = c.Descripcion
                
            });

        }


        //-----------------------------------------------------------------
        //MOSTRAR 
        // GET: api/Presentaciones/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var presentacion = await _context.Presentaciones.FindAsync(id);

            if (presentacion == null)
            {
                return NotFound();
            }

            return Ok(new PresentacionViewModel
            {
                IdPresentacion = presentacion.IdPresentacion,
                Cantidad = presentacion.Cantidad,
                Unidad = presentacion.Unidad,
                Descripcion = presentacion.Descripcion,
                Condicion = presentacion.Condicion
            });
        }

        //-----------------------------------------------------------
        //METODO ACTUALIZAR 
        // PUT: api/Presentaciones/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdPresentacion <= 0)
            {
                return BadRequest();
            }

            var presentacion = await _context.Presentaciones.FirstOrDefaultAsync(c => c.IdPresentacion == model.IdPresentacion);

            if (presentacion == null)
            {
                return NotFound();
            }

            presentacion.Cantidad = model.Cantidad;
            presentacion.Unidad = model.Unidad;
            presentacion.Descripcion = model.Descripcion;

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


        //-----------------------------------------------------------

        //METODO CREAR  
        // POST: api/Presentaciones/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Presentacion presentacion = new Presentacion
            {
                Cantidad = model.Cantidad,
                Unidad = model.Unidad,
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.Presentaciones.Add(presentacion);
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

        //-----------------------------------------------------------
        //--------ELIMINAR CATEGORIA
        
        //-----------------------------------------------------------


        //----------DESACTIVAR 
        // PUT: api/Presentaciones/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var presentacion = await _context.Presentaciones.FirstOrDefaultAsync(c => c.IdPresentacion == id);

            if (presentacion == null)
            {
                return NotFound();
            }

            presentacion.Condicion = false;

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

        //-----------------------------------------------------------


        //----------ACTIVAR 
        // PUT: api/Presentaciones/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var presentacion = await _context.Presentaciones.FirstOrDefaultAsync(c => c.IdPresentacion == id);

            if (presentacion == null)
            {
                return NotFound();
            }

            presentacion.Condicion = true;

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




        //-----------------------------------------------------------------------------
        private bool PresentacionExists(int id)
        {
            return _context.Presentaciones.Any(e => e.IdPresentacion == id);
        }
    }
}
