using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaFacturacion.Datos;
using SistemaFacturacion.Entidades.Usuarios;
using SistemaFacturacion.Web.Models.Usuarios.Usuario;

namespace SistemaFacturacion.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DbContextSistemaFacturacion _context;
        //----------------
        private readonly IConfiguration _config;  //---- 

        public UsuariosController(DbContextSistemaFacturacion context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }



        // GET: api/Usuarios/Listar
        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Include(u => u.rol).ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                IdUsuario = u.IdUsuario,
                IdRol = u.IdRol,
                Rol = u.rol.Nombre,
                Nombre = u.Nombre,
                TipoDocumento = u.TipoDocumento,
                NumeroDocumento = u.NumeroDocumento,
                Direccion = u.Direccion,
                Telefono = u.Telefono,
                Email = u.Email,
                password_hash = u.password_hash,
                Condicion = u.Condicion
            });

        }
        //----------------------------------------------------------------------------------------



        // POST: api/Usuarios/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.Email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            Usuario usuario = new Usuario
            {
                IdRol = model.IdRol,
                Nombre = model.Nombre,
                TipoDocumento = model.TipoDocumento,
                NumeroDocumento = model.NumeroDocumento,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email.ToLower(),
                password_salt = passwordSalt,
                password_hash = passwordHash,
                Condicion = true
            };

            _context.Usuarios.Add(usuario);
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
        //------------------------------------------------------------------------------------



        // PUT: api/Usuarios/Actualizar
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdUsuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.IdRol = model.IdRol;
            usuario.Nombre = model.Nombre;
            usuario.TipoDocumento = model.TipoDocumento;
            usuario.NumeroDocumento = model.NumeroDocumento;
            usuario.Direccion = model.Direccion;
            usuario.Telefono = model.Telefono;
            usuario.Email = model.Email.ToLower();

            if (model.act_password == true) // si el usuario desea cambiar pass true
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.password_hash = passwordHash;
                usuario.password_salt = passwordSalt;
            }

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
        //----------------------------------------------------------------------------------


        //metodo para encriptar el password
        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        //--------------------------------------------------------------------------------

        // PUT: api/Usuarios/Desactivar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Condicion = false;

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
        //-------------------------------------------------------------------------------------------


        // PUT: api/Usuarios/Activar/1
        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Condicion = true;

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
        //-------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------

        //-----metodo login---------------------------- 27/10/21
        // PUT: api/Usuarios/Login
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.email.ToLower();

            var usuario = await _context.Usuarios.Where(u => u.Condicion == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.password, usuario.password_hash, usuario.password_salt))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.rol.Nombre ),
                new Claim("idusuario", usuario.IdUsuario.ToString() ),
                new Claim("rol", usuario.rol.Nombre ),
                new Claim("nombre", usuario.Nombre )
            };

            return Ok(
                    new { token = GenerarToken(claims) }
                );

        }


        //verificar el pasword------------------------------------------------------------
        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }


        //generar token------------------------------------------------------------------------------------
        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //------------------------------------------------------------------------------------------------




        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
