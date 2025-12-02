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

using SistemaFacturacion.Web.Models.Almacen.CategoriaPrincipal;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Almacen,Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaPrincipalesController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public CategoriaPrincipalesController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR CATEGORIA
        // GET: api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaPrincipalViewModel>> Listar()
        {
            var categoria = await _context.CategoriaPrincipales.ToListAsync();

            return categoria.Select(c => new CategoriaPrincipalViewModel
            {
                IdCategoriaPrincipal = c.IdCategoriaPrincipal,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Condicion = c.Condicion
            });

        }
        //----------------------------------------------------------------

        // SELECCIONAR CATEGORIA
        // GET: api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var categoria = await _context.CategoriaPrincipales.Where(c => c.Condicion == true).ToListAsync();

            return categoria.Select(c => new SelectViewModel
            {
                IdCategoriaPrincipal = c.IdCategoriaPrincipal,
                Nombre = c.Nombre
            });

        }

        //-----------------------------------------------------------------
        //MOSTRAR CATEGORIA
        // GET: api/Categorias/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var categoria = await _context.CategoriaPrincipales.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaPrincipalViewModel
            {
                IdCategoriaPrincipal = categoria.IdCategoriaPrincipal,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Condicion = categoria.Condicion
            });
        }

        //-----------------------------------------------------------
        //METODO ACTUALIZAR CATEGORIA
        // PUT: api/Categorias/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdCategoriaPrincipal <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.CategoriaPrincipales
                .FirstOrDefaultAsync(c => c.IdCategoriaPrincipal == model.IdCategoriaPrincipal);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Nombre = model.Nombre;
            categoria.Descripcion = model.Descripcion;

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
        //METODO CREAR  Sucursal
        // POST: api/Sucursal/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoriaPrincipal categoria = new CategoriaPrincipal
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true

            };

            _context.CategoriaPrincipales.Add(categoria);
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
        // DELETE: api/Categorias/Eliminar/1
      

        //-----------------------------------------------------------
        //----------DESACTIVAR CATEGORIA
        // PUT: api/Categorias/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.CategoriaPrincipales
                .FirstOrDefaultAsync(c => c.IdCategoriaPrincipal == id);

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
        // PUT: api/Categorias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.CategoriaPrincipales
                .FirstOrDefaultAsync(c => c.IdCategoriaPrincipal == id);

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

        private bool CategoriaPrincipalExists(int id)
        {
            return _context.CategoriaPrincipales.Any(e => e.IdCategoriaPrincipal == id);
        }
    }
}
