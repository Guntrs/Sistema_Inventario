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
using SistemaFacturacion.Entidades.Factura;
using SistemaFacturacion.Web.Models.Factura.BitacoraEnvio;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Vendedor,Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraEnviosController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public BitacoraEnviosController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR 
        // GET: api/BitacoraEnvios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<BitacoraEnvioViewModel>> Listar()
        {
            var bitacora = await _context.Bitacoras
                .Include(a => a.ventas).ToListAsync();
              
            return bitacora.Select(c => new BitacoraEnvioViewModel
            {
                IdBitacora = c.IdBitacora,

                IdVenta = c.IdVenta,
                Total = c.ventas.Total,
                SerieSAT = c.ventas.SerieSAT,
                NumeroSAT = c.ventas.NumeroSAT,
                Estado = c.ventas.Estado,
                
                FechaEnvio = c.FechaEnvio,
                RespuestaSAT = c.RespuestaSAT,
                CodigoRespuestaSAT = c.CodigoRespuestaSAT,
                MensajeRespuestaSAT = c.MensajeRespuestaSAT
            });
        }
        //----------------------------------------------------------------

        // SELECCIONAR 
        // GET: api/BitacoraEnvios/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var bitacora = await _context.Bitacoras.ToListAsync();

            return bitacora.Select(c => new SelectViewModel
            {
                IdBitacora = c.IdBitacora,

                FechaEnvio = c.FechaEnvio,
                RespuestaSAT = c.RespuestaSAT,
                CodigoRespuestaSAT = c.CodigoRespuestaSAT,
                MensajeRespuestaSAT = c.MensajeRespuestaSAT
            });

        }

        //----------------------------------------------------------------
        //METODO CREAR  
        // POST: api//
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaenvio = DateTime.Now;

            BitacoraEnvio bitacora = new BitacoraEnvio 
            {
               

                IdVenta = model.IdVenta,

                FechaEnvio = fechaenvio,
                RespuestaSAT = model.RespuestaSAT,
                CodigoRespuestaSAT = model.CodigoRespuestaSAT,
                MensajeRespuestaSAT = model.MensajeRespuestaSAT

            };

            _context.Bitacoras.Add(bitacora);
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

        private bool BitacoraEnvioExists(int id)
        {
            return _context.Bitacoras.Any(e => e.IdBitacora == id);
        }
    }
}
