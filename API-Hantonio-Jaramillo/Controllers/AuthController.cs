using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
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
    private readonly ApplicationDbContext _context; 

    public AuthController(IConfiguration config, ApplicationDbContext context)
    {
        _config = config;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.NombreUsuario) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Usuario y password son requeridos." });

        var normalizedLogin = request.NombreUsuario.Trim();

        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal) 
            .FirstOrDefaultAsync(u => u.NombreUsuario == normalizedLogin && u.Estatus);

        if (usuario is null)
            return Unauthorized(new { message = "Credenciales inválidas." });

        var hashed = HashPassword(request.Password);
        if (usuario.PasswordHash != hashed)
            return Unauthorized(new { message = "Credenciales inválidas." });

        usuario.UltimoLogin = DateTime.Now;
        await _context.SaveChangesAsync();

        string roleName = usuario.Rol?.Nombre ?? "Empleado";

        var expires = DateTime.Now.AddDays(30);

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
    [Authorize] 
    public async Task<IActionResult> Logout()
    {
        var nombreUsuario = User.Identity?.Name;

        if (string.IsNullOrEmpty(nombreUsuario))
            return BadRequest("No se pudo identificar al usuario.");

     
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        if (usuario != null)
        {
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