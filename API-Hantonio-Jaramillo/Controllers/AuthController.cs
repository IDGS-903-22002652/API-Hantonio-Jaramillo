using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
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
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Login y password son obligatorios.");
        }

        // Buscar usuario por login
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Login == request.Login && u.Activo);

        if (usuario is null)
        {
            return Unauthorized("Credenciales inválidas.");
        }

        // Verificar contraseña
        var hashedPassword = HashPassword(request.Password);
        if (usuario.PasswordHash != hashedPassword)
        {
            return Unauthorized("Credenciales inválidas.");
        }

        // Actualizar último login
        usuario.UltimoLogin = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generar token JWT
        var token = GenerateJwtToken(usuario);

        return Ok(new LoginResponse
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:ExpireMinutes"]!)),
            Usuario = new UsuarioInfo
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Login = usuario.Login,
                Rol = usuario.Rol?.Nombre
            }
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(request.IdUsuario);
        if (usuario is null)
        {
            return NotFound("Usuario no encontrado.");
        }

        usuario.UltimoLogout = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Sesión cerrada correctamente." });
    }

    private string GenerateJwtToken(Models.Usuario usuario)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, usuario.Login),
            new Claim(ClaimTypes.GivenName, usuario.NombreCompleto ?? ""),
            new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "Usuario"),
            new Claim("IdSucursal", usuario.IdSucursal?.ToString() ?? "")
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["ExpireMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
