using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API_Hantonio_Jaramillo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _context; // Cambiado al nuevo Context

    public AuthController(IConfiguration config, ApplicationDbContext context)
    {
        _config = config;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        // Usamos NombreUsuario en lugar de Login para coincidir con el nuevo DTO/Modelo
        if (string.IsNullOrWhiteSpace(request.NombreUsuario) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Usuario y password son requeridos." });

        var normalizedLogin = request.NombreUsuario.Trim();

        // Buscamos por NombreUsuario y verificamos Estatus (bool)
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal) // Incluimos sucursal para el Response
            .FirstOrDefaultAsync(u => u.NombreUsuario == normalizedLogin && u.Estatus);

        if (usuario is null)
            return Unauthorized(new { message = "Credenciales inválidas." });

        // Verificación de Hash
        var hashed = HashPassword(request.Password);
        if (usuario.PasswordHash != hashed)
            return Unauthorized(new { message = "Credenciales inválidas." });

        // Actualizar último login
        usuario.UltimoLogin = DateTime.Now;
        await _context.SaveChangesAsync();

        // Obtener nombre del rol
        string roleName = usuario.Rol?.Nombre ?? "Empleado";

        // Configuración de expiración desde appsettings o default 60 min
        var expireMinutes = double.Parse(_config["Jwt:ExpireMinutes"] ?? "60");
        var expires = DateTime.Now.AddMinutes(expireMinutes);

        var token = BuildToken(usuario.NombreUsuario, roleName, usuario.IdSucursal.ToString(), expires);

        return Ok(new LoginResponse
        {
            Token = token,
            Expiration = expires,
            Usuario = new UsuarioInfo
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                NombreUsuario = usuario.NombreUsuario,
                Rol = roleName,
                IdSucursal = usuario.IdSucursal,
                NombreSucursal = usuario.Sucursal?.Nombre
            }
        });
    }

    [HttpPost("logout")]
    [Authorize] // Solo un usuario logueado puede cerrar sesión
    public async Task<IActionResult> Logout()
    {
        // 1. Obtener el NombreUsuario del Token (Claim)
        var nombreUsuario = User.Identity?.Name;

        if (string.IsNullOrEmpty(nombreUsuario))
            return BadRequest("No se pudo identificar al usuario.");

        // 2. Buscar al usuario en la base de datos
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        if (usuario != null)
        {
            // 3. Registrar la fecha de salida
            usuario.UltimoLogout = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        return Ok(new { message = "Cierre de sesión registrado exitosamente." });
    }

    private string BuildToken(string username, string role, string? sucursalId, DateTime expires)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim("IdSucursal", sucursalId ?? "0"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password ?? string.Empty));
        return Convert.ToBase64String(bytes);
    }
}