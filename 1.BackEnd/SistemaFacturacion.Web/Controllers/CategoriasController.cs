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

using SistemaFacturacion.Web.Models.Almacen.Categoria;

namespace SistemaFacturacion.Web.Controllers
{

    [Authorize(Roles = "Almacen,Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public CategoriasController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }
    

        //LISTAR CATEGORIA
        // GET: api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaViewModel>> Listar()
        {
            var categoria = await _context.Categorias.ToListAsync();

            var tipo = await _context.Categorias
                .Include(a => a.CategoriaSecundaria).ToListAsync();

            return categoria.Select(c => new CategoriaViewModel
            {
                IdCategoria = c.IdCategoria,

                IdCategoriaSecundaria = c.IdCategoriaSecundaria,
                CategoriaSecundaria = c.CategoriaSecundaria.Nombre,

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
            var categoria = await _context.Categorias.Where(c => c.Condicion == true).ToListAsync();

            return categoria.Select(c => new SelectViewModel
            {
                IdCategoria = c.IdCategoria,
                Nombre = c.Nombre
            });

        }

        //-----------------------------------------------------------------
        //MOSTRAR CATEGORIA
        // GET: api/Categorias/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaViewModel
            {
                IdCategoria = categoria.IdCategoria,

                IdCategoriaSecundaria = categoria.IdCategoriaSecundaria,

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

            if (model.IdCategoria <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == model.IdCategoria);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.IdCategoriaSecundaria = model.IdCategoriaSecundaria;

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

            Categoria categoria = new Categoria
            {
                IdCategoriaSecundaria = model.IdCategoriaSecundaria,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true

            };

            _context.Categorias.Add(categoria);
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
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
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

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);

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

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == id);

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


        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id);
        }
    }
}
