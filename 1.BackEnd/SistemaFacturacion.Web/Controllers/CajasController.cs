using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Sucursal;
using SistemaFacturacion.Web.Models.Sucursal.Caja;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Administrador")]

    [Route("api/[controller]")]
    [ApiController]
    public class CajasController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public CajasController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR Caja
        // GET: api/Caja/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CajaViewModel>> Listar()
        {
            var caja = await _context.Cajas.ToListAsync();

            var tipo = await _context.Cajas.Include(a => a.Sucursal).ToListAsync();

            return caja.Select(s => new CajaViewModel
            {
                IdCaja = s.IdCaja,

                IdSucursal = s.IdSucursal,
                Sucursal = s.Sucursal.Nombre,

                Descripcion = s.Descripcion,
                Condicion = s.Condicion
            });
        }


        // SELECCIONAR Caja
        // GET: api/Caja/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var caja = await _context.Cajas.Where(c => c.Condicion == true).ToListAsync();

            return caja.Select(c => new SelectViewModel
            {
                IdCaja = c.IdCaja,
                Descripcion = c.Descripcion
            });

        }
        //------------------------------------------------


        //MOSTRAR Caja
        // GET: api/Caja/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var caja = await _context.Cajas.FindAsync(id);

            if (caja == null)
            {
                return NotFound();
            }

            return Ok(new CajaViewModel
            {
                IdSucursal = caja.IdSucursal,
                Descripcion = caja.Descripcion,
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

            if (model.IdCaja <= 0)
            {
                return BadRequest();
            }

            var sucursal = await _context.Cajas.FirstOrDefaultAsync(c => c.IdCaja == model.IdCaja);

            if (sucursal == null)
            {
                return NotFound();
            }

            sucursal.IdSucursal = model.IdSucursal;
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

            Caja articulo = new Caja
            {

                IdSucursal = model.IdSucursal,             
                Descripcion = model.Descripcion,
                Condicion = true
            };

            _context.Cajas.Add(articulo);
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

            var caja = await _context.Cajas.FindAsync(id);
            if (caja == null)
            {
                return NotFound();
            }

            _context.Cajas.Remove(caja);
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

            var caja = await _context.Cajas.FirstOrDefaultAsync(c => c.IdCaja == id);

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

            var caja = await _context.Cajas.FirstOrDefaultAsync(c => c.IdCaja == id);

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




        private bool CajaExists(int id)
        {
            return _context.Cajas.Any(e => e.IdCaja == id);
        }
    }
}
