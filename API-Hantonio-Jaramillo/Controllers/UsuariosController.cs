using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Hantonio_Jaramillo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/usuarios
    [HttpGet]
    public async Task<ActionResult<List<UsuarioDtoResponse>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal)
            .Select(u => new UsuarioDtoResponse
            {
                IdUsuario = u.IdUsuario,
                NombreCompleto = u.NombreCompleto,
                Login = u.Login,
                IdRol = u.IdRol,
                NombreRol = u.Rol != null ? u.Rol.Nombre : null,
                IdSucursal = u.IdSucursal,
                NombreSucursal = u.Sucursal != null ? u.Sucursal.Nombre : null,
                Activo = u.Activo,
                UltimoLogin = u.UltimoLogin
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    // GET: api/usuarios/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDtoResponse>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal)
            .FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (usuario == null)
            return NotFound(new { message = "Usuario no encontrado" });

        var usuarioDto = new UsuarioDtoResponse
        {
            IdUsuario = usuario.IdUsuario,
            NombreCompleto = usuario.NombreCompleto,
            Login = usuario.Login,
            IdRol = usuario.IdRol,
            NombreRol = usuario.Rol?.Nombre,
            IdSucursal = usuario.IdSucursal,
            NombreSucursal = usuario.Sucursal?.Nombre,
            Activo = usuario.Activo,
            UltimoLogin = usuario.UltimoLogin
        };

        return Ok(usuarioDto);
    }

    // POST: api/usuarios
    [HttpPost]
    public async Task<ActionResult<UsuarioDtoResponse>> CreateUsuario([FromBody] UsuarioDtoCreateRequest request)
    {
        // Normalizar login
        var normalizedLogin = request.Login?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedLogin))
            return BadRequest(new { message = "El campo 'login' es obligatorio." });

        // Validar que el login no exista
        if (await _context.Usuarios.AnyAsync(u => u.Login == normalizedLogin))
            return Conflict(new { message = "El usuario ya existe" });

        var usuario = new Usuario
        {
            Login = normalizedLogin,
            PasswordHash = HashPassword(request.Password),
            NombreCompleto = request.NombreCompleto,
            IdRol = request.IdRol,
            IdSucursal = request.IdSucursal,
            Activo = true
        };

        _context.Usuarios.Add(usuario);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Login == normalizedLogin))
                return Conflict(new { message = "El usuario ya existe" });

            throw;
        }

        await _context.Entry(usuario).Reference(u => u.Rol).LoadAsync();
        await _context.Entry(usuario).Reference(u => u.Sucursal).LoadAsync();

        var usuarioDto = MapearUsuarioADto(usuario);
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuarioDto);
    }

    // PUT: api/usuarios/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDtoUpdateRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound(new { message = "Usuario no encontrado" });

        if (!string.IsNullOrEmpty(request.NombreCompleto))
            usuario.NombreCompleto = request.NombreCompleto;

        if (request.IdRol.HasValue)
            usuario.IdRol = request.IdRol;

        if (request.IdSucursal.HasValue)
            usuario.IdSucursal = request.IdSucursal;

        if (request.Activo.HasValue)
            usuario.Activo = request.Activo.Value;

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        await _context.Entry(usuario).Reference(u => u.Rol).LoadAsync();
        await _context.Entry(usuario).Reference(u => u.Sucursal).LoadAsync();

        var usuarioDto = MapearUsuarioADto(usuario);
        return Ok(usuarioDto);
    }

    // PATCH: api/usuarios/{id}/estado
    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] UsuarioDtoCambiarEstado request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound(new { message = "Usuario no encontrado" });

        usuario.Activo = request.Activo;
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Usuario {(request.Activo ? "activado" : "desactivado")}" });
    }

    // DELETE: api/usuarios/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound(new { message = "Usuario no encontrado" });

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Métodos auxiliares
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private UsuarioDtoResponse MapearUsuarioADto(Usuario usuario)
    {
        return new UsuarioDtoResponse
        {
            IdUsuario = usuario.IdUsuario,
            NombreCompleto = usuario.NombreCompleto,
            Login = usuario.Login,
            IdRol = usuario.IdRol,
            NombreRol = usuario.Rol?.Nombre,
            IdSucursal = usuario.IdSucursal,
            NombreSucursal = usuario.Sucursal?.Nombre,
            Activo = usuario.Activo,
            UltimoLogin = usuario.UltimoLogin
        };
    }
}