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
using SistemaFacturacion.Web.Models.Sucursal.Sucursal;

namespace SistemaFacturacion.Web.Controllers

{
    [Authorize(Roles = "Almacen,Administrador")]

    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public SucursalesController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR Sucursal
        // GET: api/Sucursales/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SucursalViewModel>> Listar()
        {
            var sucursal = await _context.Sucursales.ToListAsync();

            return sucursal.Select(s => new SucursalViewModel
            {
                IdSucursal = s.IdSucursal,
                Nombre = s.Nombre,
                Codigo = s.Codigo,
                Direccion = s.Direccion,
                Telefono = s.Telefono,
                Email = s.Email,
                Condicion = s.Condicion
            });

        }


        // SELECCIONAR Sucursal
        // GET: api/Sucursales/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var categoria = await _context.Sucursales.Where(c => c.Condicion == true).ToListAsync();

            return categoria.Select(c => new SelectViewModel
            {
                IdSucursal = c.IdSucursal,
                Nombre = c.Nombre
            });

        }
        //------------------------------------------------


        //MOSTRAR Sucursal
        // GET: api/Sucursales/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var sucursal = await _context.Sucursales.FindAsync(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return Ok(new SucursalViewModel
            {
                IdSucursal = sucursal.IdSucursal,
                Nombre = sucursal.Nombre,
                Codigo = sucursal.Codigo,
                Direccion = sucursal.Direccion,
                Telefono = sucursal.Telefono,
                Email = sucursal.Email,
                Condicion = sucursal.Condicion
            });
        }
        //---------------------------------

        //METODO ACTUALIZAR Sucursal
        // PUT: api/Sucursales/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid) //validar el modelo si no se cumple un dato
            {
                return BadRequest(ModelState);
            }

            if (model.IdSucursal <= 0)
            {
                return BadRequest();
            }

            var sucursal = await _context.Sucursales.FirstOrDefaultAsync(c => c.IdSucursal == model.IdSucursal);

            if (sucursal == null)
            {
                return NotFound();
            }

            sucursal.Nombre = model.Nombre;
            sucursal.Codigo = model.Codigo;
            sucursal.Direccion = model.Direccion;
            sucursal.Telefono = model.Telefono;
            sucursal.Email = model.Email;


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


        //METODO CREAR  Sucursal
        // POST: api/Sucursal/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Sucursal1 sucursal = new Sucursal1
            {
                Nombre = model.Nombre,
                Codigo = model.Codigo,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email,
                Condicion = true

            };

            _context.Sucursales.Add(sucursal);
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


        //--------ELIMINAR sucursal
        // DELETE: api/Sucursales/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursales.Remove(sucursal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(sucursal);
        }
        //--------------------------------


        //----------DESACTIVAR Sucursal
        
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var sucursal = await _context.Sucursales.FirstOrDefaultAsync(c => c.IdSucursal == id);

            if (sucursal == null)
            {
                return NotFound();
            }

            sucursal.Condicion = false;

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

        //----------ACTIVAR Sucursal
        
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var sucursal = await _context.Sucursales.FirstOrDefaultAsync(c => c.IdSucursal == id);

            if (sucursal == null)
            {
                return NotFound();
            }

            sucursal.Condicion = true;

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
        //-------------------------------*/****************************************


        private bool Sucursal1Exists(int id)
        {
            return _context.Sucursales.Any(e => e.IdSucursal == id);
        }
    }
}
