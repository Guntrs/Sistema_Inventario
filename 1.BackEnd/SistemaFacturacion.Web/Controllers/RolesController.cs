using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Usuarios;
using SistemaFacturacion.Web.Models.Usuarios.Rol;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public RolesController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }


        //LISTAR
        // GET: api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(c => new RolViewModel
            {
                IdRol = c.IdRol,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Condicion = c.Condicion
            });

        }
        //----------------------------------------------------------------

        // SELECCIONAR 
        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var rol = await _context.Roles.Where(c => c.Condicion == true).ToListAsync();

            return rol.Select(c => new SelectViewModel
            {
                IdRol = c.IdRol,
                Nombre = c.Nombre
            });

        }
        //-----------------------------------------------------------------

        //MOSTRAR 
        // GET: api/Roles/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return Ok(new RolViewModel
            {
                IdRol = rol.IdRol,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                Condicion = rol.Condicion
            });
        }
        //-----------------------------------------------------------

        //METODO ACTUALIZAR 
        // PUT: api/Roles/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdRol <= 0)
            {
                return BadRequest();
            }

            var rol = await _context.Roles.FirstOrDefaultAsync(c => c.IdRol == model.IdRol);

            if (rol == null)
            {
                return NotFound();
            }

            rol.Nombre = model.Nombre;
            rol.Descripcion = model.Descripcion;

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
        // POST: api/Roles/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rol rol = new Rol
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.Roles.Add(rol);
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

        //DESACTIVAR 
        // PUT: api/Roles/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Roles.FirstOrDefaultAsync(c => c.IdRol == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Condicion = false;

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

        //----------ACTIVAR CATEGORIA
        // PUT: api/Roles/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Roles.FirstOrDefaultAsync(c => c.IdRol == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Condicion = true;

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


        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.IdRol == id);
        }
    }
}
