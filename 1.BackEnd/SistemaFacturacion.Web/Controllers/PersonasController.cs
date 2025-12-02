using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Ventas;
using SistemaFacturacion.Web.Models.Ventas.Persona;

namespace SistemaFacturacion.Web.Controllers
{

    
    
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public PersonasController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }


        // GET: api/Personas/ListarClientes
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarClientes()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Cliente").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                IdPersona = p.IdPersona,
                TipoPersona = p.TipoPersona,
                Nombre = p.Nombre,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email,
                CodigoPersona = p.CodigoPersona
            });

        }
        //-------------------------------------------------

        // GET: api/Personas/ListarProveedores
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarProveedores()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Proveedor").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                IdPersona = p.IdPersona,
                TipoPersona = p.TipoPersona,
                Nombre = p.Nombre,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Email = p.Email,
                CodigoPersona = p.CodigoPersona
            });

        }
        //------------------------------------------------


        //SELECT PROVEEDORES
        // GET: api/Personas/SelectProveedores
        [Authorize(Roles = "Almacen,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectProveedores()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Proveedor").ToListAsync();

            return persona.Select(p => new SelectViewModel
            {
                IdPersona = p.IdPersona,
                Nombre = p.Nombre,
                CodigoPersona = p.CodigoPersona,

                TipoDocumento = p.TipoDocumento,
                NumeroDocumento= p.NumeroDocumento,
                Direccion= p.Direccion
            });

        }

        //SELECT CLIENTES
        // GET: api/Personas/SelectClientes
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectClientes()
        {
            var persona = await _context.Personas.Where(p => p.TipoPersona == "Cliente").ToListAsync();

            return persona.Select(p => new SelectViewModel
            {
                IdPersona = p.IdPersona,
                Nombre = p.Nombre,
                CodigoPersona = p.CodigoPersona,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento,
                Direccion = p.Direccion
            });

        }
        //------------------------------------------------------------------------------------------

        //Mostrar Cliente por Nit       
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarClienteNit([FromRoute] string numdocumento)
        {

            var cliente = await _context.Personas.SingleOrDefaultAsync(a => a.NumeroDocumento == numdocumento);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(new PersonaViewModel
            {
                IdPersona = cliente.IdPersona,
                TipoPersona = cliente.TipoPersona,
                Nombre = cliente.Nombre,
                TipoDocumento = cliente.TipoDocumento,
                NumeroDocumento = cliente.NumeroDocumento,
            });
        }
        //------------------------------------------------

        // POST: api/Personas/Crear
        [Authorize(Roles = "Almacen,Vendedor,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.Email.ToLower();

            if (await _context.Personas.AnyAsync(p => p.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            Persona persona = new Persona
            {
                TipoPersona = model.TipoPersona,
                Nombre = model.Nombre,
                TipoDocumento = model.TipoDocumento,
                NumeroDocumento = model.NumeroDocumento,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                CodigoPersona = model.CodigoPersona,
                Email = model.Email.ToLower()
            };

            _context.Personas.Add(persona);
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
        //-------------------------------------------------


        // PUT: api/Personas/Actualizar
        [Authorize(Roles = "Almacen,Administrador,Vendedor")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdPersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.IdPersona == model.IdPersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.TipoPersona = model.TipoPersona;
            persona.Nombre = model.Nombre;
            persona.TipoDocumento = model.TipoDocumento;
            persona.NumeroDocumento = model.NumeroDocumento;
            persona.Direccion = model.Direccion;
            persona.Telefono = model.Telefono;
            persona.CodigoPersona = model.CodigoPersona;
            persona.Email = model.Email.ToLower();

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





        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.IdPersona == id);
        }
    }
}
