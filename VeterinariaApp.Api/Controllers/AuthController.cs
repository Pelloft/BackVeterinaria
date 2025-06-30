using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;

namespace VeterinariaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly VeterinariaDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(VeterinariaDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserAuthDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo))
                return BadRequest("El correo ya está registrado.");

            var usuario = new Usuario
            {
                NombreUsuario = dto.Nombre,
                Correo = dto.Correo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Clave)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == dto.Correo);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            // Verifica si está bloqueado
            if (usuario.Bloqueado.HasValue && usuario.Bloqueado.Value > DateTime.Now)
            {
                var minutosRestantes = (usuario.Bloqueado.Value - DateTime.Now).Minutes;
                return Unauthorized($"Usuario bloqueado. Intenta de nuevo en {minutosRestantes} minutos.");
            }

            // Verifica contraseña
            if (!BCrypt.Net.BCrypt.Verify(dto.Clave, usuario.PasswordHash))
            {
                usuario.IntentosFallidos++;

                if (usuario.IntentosFallidos >= 3)
                {
                    usuario.Bloqueado = DateTime.Now.AddMinutes(3); // Bloqueado por 3 min
                    usuario.IntentosFallidos = 0; // Reseteamos el contador para la próxima vez
                }

                await _context.SaveChangesAsync();
                return Unauthorized("Contraseña incorrecta.");
            }

            // Login exitoso: reiniciar bloqueo e intentos
            usuario.IntentosFallidos = 0;
            usuario.Bloqueado = null;
            await _context.SaveChangesAsync();

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Email, usuario.Correo)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresInMinutes"]!)),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }


    }
}
