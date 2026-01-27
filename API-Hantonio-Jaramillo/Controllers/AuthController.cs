
using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

namespace API_Hantonio_Jaramillo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _context;

    public AuthController(IConfiguration config, AppDbContext context)
    {
        _config = config;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Login y password son requeridos." });

        var normalizedLogin = request.Login.Trim();

        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Login == normalizedLogin && u.Activo);

        if (usuario is null)
            return Unauthorized(new { message = "Credenciales inválidas." });

        var hashed = HashPassword(request.Password);
        if (usuario.PasswordHash != hashed)
            return Unauthorized(new { message = "Credenciales inválidas." });

        // actualizar ultimo_login
        usuario.UltimoLogin = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Resolver nombre del rol: primero por navegación, si no existe, por IdRol
        string? roleName = usuario.Rol?.Nombre;
        if (string.IsNullOrWhiteSpace(roleName) && usuario.IdRol.HasValue)
        {
            roleName = await _context.Roles
                .Where(r => r.IdRol == usuario.IdRol.Value)
                .Select(r => r.Nombre)
                .FirstOrDefaultAsync();
        }

        roleName ??= string.Empty; // garantizar no-null

        var expires = DateTime.UtcNow.AddHours(4);
        var token = BuildToken(usuario.Login, roleName, _config, expires);

        var response = new LoginResponse
        {
            Token = token,
            Expiration = expires,
            Usuario = new UsuarioInfo
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Login = usuario.Login,
                Rol = roleName
            }
        };

        return Ok(response);
    }

    private string BuildToken(string username, string role, IConfiguration config, DateTime expires)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // No normalices, usa el 'role' que viene directamente de la DB ("ADMIN")
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, username),
        new Claim(ClaimTypes.Name, username),
        // IMPORTANTE: Usamos ClaimTypes.Role para que coincida con RoleClaimType en Program.cs
        new Claim(ClaimTypes.Role, role),
        new Claim("role", role), // Por si acaso para el frontend
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
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