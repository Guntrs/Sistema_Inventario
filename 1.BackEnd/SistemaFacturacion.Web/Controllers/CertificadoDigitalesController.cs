using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Factura;
using SistemaFacturacion.Web.Models.Factura.BitacoraEnvio;
using SistemaFacturacion.Web.Models.Factura.CertificadoDigital;
using CrearViewModel = SistemaFacturacion.Web.Models.Factura.CertificadoDigital.CrearViewModel;
using SelectViewModel = SistemaFacturacion.Web.Models.Factura.CertificadoDigital.SelectViewModel;

namespace SistemaFacturacion.Web.Controllers
{
    [Authorize(Roles = "Vendedor,Administrador")]
    [Route("api/[controller]")]
    public class CertificadoDigitalesController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;

        public CertificadoDigitalesController(DbContextSistemaFacturacion context)
        {
            _context = context;
        }

        //LISTAR 
        // GET: api/BitacoraEnvios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CertificadoDigitalViewModel>> Listar()
        {
            var certificado = await _context.Certificados
                .Include(a => a.usuario)
                .ThenInclude(a => a.rol)
                
                .ToListAsync();

            return certificado.Select(c => new CertificadoDigitalViewModel
            {
                IdCertificado = c.IdCertificado,

                IdUsuario = c.IdUsuario,
                NombreUsuario = c.usuario.Nombre,
                Rol = c.usuario.rol.Nombre,

                Nombre = c.Nombre,
                FechaExpiracion = c.FechaExpiracion,
                ArchivoCertificado = c.ArchivoCertificado,
                FechaCreacion = c.FechaCreacion
            });

        }
        //----------------------------------------------------------------

        // SELECCIONAR 
        // GET: api/BitacoraEnvios/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var bitacora = await _context.Certificados.ToListAsync();

            return bitacora.Select(c => new SelectViewModel
            {
                IdCertificado = c.IdCertificado,

              
                Nombre = c.Nombre,
                FechaExpiracion = c.FechaExpiracion,
                ArchivoCertificado = c.ArchivoCertificado,
                FechaCreacion = c.FechaCreacion
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
            var fechacreacion = DateTime.Now;

            CertificadoDigital certificado = new CertificadoDigital
            {

               

                IdUsuario = model.IdUsuario,

                Nombre = model.Nombre,
                FechaExpiracion = model.FechaExpiracion,
                ArchivoCertificado = model.ArchivoCertificado,
                FechaCreacion = fechacreacion


               

            };

            _context.Certificados.Add(certificado);
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




        private bool CertificadoDigitalExists(int id)
        {
            return _context.Certificados.Any(e => e.IdCertificado == id);
        }
    }
}
