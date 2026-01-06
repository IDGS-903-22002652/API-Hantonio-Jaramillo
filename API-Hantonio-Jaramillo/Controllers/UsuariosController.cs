using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Hantonio_Jaramillo.Controllers;

[Authorize(Roles = "Administrador")]
[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal)
            .FirstOrDefaultAsync(u => u.IdUsuario == id);

        return usuario is null ? NotFound() : usuario;
    }

    [HttpPost]
    [AllowAnonymous] // Permitir registro sin autenticación
    public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
    {
        // Validar que el login no esté vacío
        if (string.IsNullOrWhiteSpace(usuario.Login))
        {
            return BadRequest("El campo 'login' es obligatorio.");
        }

        // Verificar si ya existe un usuario con ese login
        if (await _context.Usuarios.AnyAsync(u => u.Login == usuario.Login))
        {
            return Conflict($"Ya existe un usuario con el login '{usuario.Login}'.");
        }

        // Validar que la sucursal exista si se proporciona
        if (usuario.IdSucursal.HasValue && 
            !await _context.Sucursales.AnyAsync(s => s.IdSucursal == usuario.IdSucursal.Value))
        {
            return BadRequest($"La sucursal con ID '{usuario.IdSucursal}' no existe.");
        }

        // Similar validación para IdRol si aplica
        if (usuario.IdRol.HasValue && 
            !await _context.Roles.AnyAsync(r => r.IdRol == usuario.IdRol.Value))
        {
            return BadRequest($"El rol con ID '{usuario.IdRol}' no existe.");
        }

        // Encriptar la contraseña antes de guardar
        usuario.PasswordHash = HashPassword(usuario.PasswordHash);
        usuario.Activo = true;

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
    {
        if (id != usuario.IdUsuario)
        {
            return BadRequest("El ID no coincide.");
        }

        var existingUser = await _context.Usuarios.FindAsync(id);
        if (existingUser is null)
        {
            return NotFound();
        }

        // Solo actualizar campos permitidos (no la contraseña aquí)
        existingUser.NombreCompleto = usuario.NombreCompleto;
        existingUser.IdRol = usuario.IdRol;
        existingUser.IdSucursal = usuario.IdSucursal;
        existingUser.Activo = usuario.Activo;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Error de concurrencia al actualizar.");
        }

        return NoContent();
    }

    [HttpPatch("{id}/password")]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(request.NewPassword))
        {
            return BadRequest("La nueva contraseña es obligatoria.");
        }

        usuario.PasswordHash = HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}

public class ChangePasswordRequest
{
    public string NewPassword { get; set; } = string.Empty;
}