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
using SistemaFacturacion.Web.Models.Almacen.Marca;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Almacen,Administrador")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public MarcasController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }



        //--------------------------------------------------------------------------

        //LISTAR 
        // GET: api/Marcas/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MarcaViewModel>> Listar()
        {
            var marca = await _context.Marcas.ToListAsync();

            return marca.Select(c => new MarcaViewModel
            {
                IdMarca = c.IdMarca,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Condicion = c.Condicion
            });

        }
        //----------------------------------------------------------------

        // SELECCIONAR
        // GET: api/Marcas/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var marca = await _context.Marcas.Where(c => c.Condicion == true).ToListAsync();

            return marca.Select(c => new SelectViewModel
            {
                IdMarca = c.IdMarca,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            });

        }
        //-----------------------------------------------------------------

        //MOSTRAR 
        // GET: api/Marcas/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return Ok(new MarcaViewModel
            {
                IdMarca = marca.IdMarca,              
                Nombre = marca.Nombre,
                Descripcion = marca.Descripcion,
                Condicion = marca.Condicion
            });
        }
        //-----------------------------------------------------------

        //METODO ACTUALIZAR 
        // PUT: api/Marcas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdMarca <= 0)
            {
                return BadRequest();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(c => c.IdMarca == model.IdMarca);

            if (marca == null)
            {
                return NotFound();
            }

            marca.Nombre = model.Nombre;
            marca.Descripcion = model.Descripcion;

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
        // POST: api/Marcas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Marca marca = new Marca
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.Marcas.Add(marca);
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


        //--------ELIMINAR 

        //-----------------------------------------------------------


        //----------DESACTIVAR 
        // PUT: api/Marcas/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(c => c.IdMarca == id);

            if (marca == null)
            {
                return NotFound();
            }

            marca.Condicion = false;

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
        // PUT: api/Marcas/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(c => c.IdMarca == id);

            if (marca == null)
            {
                return NotFound();
            }

            marca.Condicion = true;

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



        private bool MarcaExists(int id)
        {
            return _context.Marcas.Any(e => e.IdMarca == id);
        }
    }
}
