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
using SistemaFacturacion.Entidades.Sucursal;

using SistemaFacturacion.Web.Models.Almacen.CategoriaSecundaria;


namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Almacen,Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaSecundariasController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public CategoriaSecundariasController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR Caja
        // GET: api/Caja/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaSecundariaViewModel>> Listar()
        {
            var caja = await _context.CategoriaSecundarias.ToListAsync();

            var tipo = await _context.CategoriaSecundarias.
                Include(a => a.CategoriaPrincipal).ToListAsync();

            return caja.Select(s => new CategoriaSecundariaViewModel
            {
                IdCategoriaSecundaria = s.IdCategoriaSecundaria,
                
                //valores de la categoria principal
                IdCategoriaPrincipal = s.IdCategoriaPrincipal,
                CategoriaPrincipal = s.CategoriaPrincipal.Nombre,

                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                Condicion = s.Condicion
            });
        }


        // SELECCIONAR Caja
        // GET: api/Caja/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var caja = await _context.CategoriaSecundarias.Where(c => c.Condicion == true).ToListAsync();

            return caja.Select(c => new SelectViewModel
            {
                IdCategoriaSecundaria = c.IdCategoriaSecundaria,
                Nombre = c.Nombre
            });

        }
        //------------------------------------------------


        //MOSTRAR Caja
        // GET: api/Caja/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var caja = await _context.CategoriaSecundarias.FindAsync(id);

            if (caja == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaSecundariaViewModel
            {
                IdCategoriaSecundaria = caja.IdCategoriaSecundaria,
                Nombre = caja.Nombre,
                Condicion = caja.Condicion
            });
        }
        //---------------------------------


        //METODO ACTUALIZAR Caja
        // PUT: api/Caja/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdCategoriaSecundaria <= 0)
            {
                return BadRequest();
            }

            var sucursal = await _context.CategoriaSecundarias.
                FirstOrDefaultAsync(c => c.IdCategoriaSecundaria == model.IdCategoriaSecundaria);

            if (sucursal == null)
            {
                return NotFound();
            }

            sucursal.IdCategoriaPrincipal= model.IdCategoriaPrincipal;
            sucursal.Nombre = model.Nombre;
            sucursal.Descripcion = model.Descripcion;

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
        //------------------------------------------------------------------------------------


        // POST: api/Caja/Crear
        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoriaSecundaria articulo = new CategoriaSecundaria
            {

                IdCategoriaPrincipal = model.IdCategoriaPrincipal,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.CategoriaSecundarias.Add(articulo);
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


        //--------ELIMINAR Caja
        // DELETE: api/Caja/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var caja = await _context.CategoriaSecundarias.FindAsync(id);
            if (caja == null)
            {
                return NotFound();
            }

            _context.CategoriaSecundarias.Remove(caja);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(caja);
        }
        //--------------------------------


        //----------DESACTIVAR Caja
        // PUT: api/Categorias/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var caja = await _context.CategoriaSecundarias.
                FirstOrDefaultAsync(c => c.IdCategoriaSecundaria == id);

            if (caja == null)
            {
                return NotFound();
            }

            caja.Condicion = false;

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

        //----------ACTIVAR CATEGORIA
        // PUT: api/KreaCategorias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var caja = await _context.CategoriaSecundarias
                .FirstOrDefaultAsync(c => c.IdCategoriaSecundaria == id);

            if (caja == null)
            {
                return NotFound();
            }

            caja.Condicion = true;

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





        private bool CategoriaSecundariaExists(int id)
        {
            return _context.CategoriaSecundarias.Any(e => e.IdCategoriaSecundaria == id);
        }
    }
}
