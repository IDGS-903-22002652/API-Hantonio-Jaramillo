using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Hantonio_Jaramillo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // --- 1. REGISTRAR USUARIO ---
    [HttpPost("registrar")]
    [Authorize(Roles = "Administrador")]
    [AllowAnonymous] // Permitir crear al primer admin; después puedes protegerlo con [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> Registrar([FromBody] UsuarioDtoCreateRequest request)
    {
        if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == request.NombreUsuario))
            return BadRequest("El nombre de usuario ya está registrado.");

        var usuario = new Usuario
        {
            NombreUsuario = request.NombreUsuario,
            NombreCompleto = request.NombreCompleto,
            Email = request.Email,
            IdRol = request.IdRol,
            IdSucursal = request.IdSucursal,
            Estatus = true,
            // Usamos exactamente el mismo método de hasheo que en AuthController
            PasswordHash = HashPassword(request.Password)
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario creado exitosamente.", id = usuario.IdUsuario });
    }

    // --- 2. LISTAR USUARIOS ---
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<IEnumerable<UsuarioDtoResponse>>> GetUsuarios()
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Sucursal)
            .Select(u => new UsuarioDtoResponse
            {
                IdUsuario = u.IdUsuario,
                NombreCompleto = u.NombreCompleto,
                NombreUsuario = u.NombreUsuario,
                Email = u.Email,
                IdRol = u.IdRol,
                NombreRol = u.Rol != null ? u.Rol.Nombre : "N/A",
                IdSucursal = u.IdSucursal,
                NombreSucursal = u.Sucursal != null ? u.Sucursal.Nombre : "N/A",
                Estatus = u.Estatus,
                UltimoLogin = u.UltimoLogin
            }).ToListAsync();
    }

    // --- 3. ACTUALIZAR USUARIO ---
    // --- 3. ACTUALIZAR USUARIO (Modificado) ---
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDtoUpdateRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        // 1. ACTUALIZAR NOMBRE DE USUARIO (Si cambió)
        if (!string.IsNullOrEmpty(request.NombreUsuario) && request.NombreUsuario != usuario.NombreUsuario)
        {
            // Validar que no exista otro usuario con ese nombre
            bool existe = await _context.Usuarios.AnyAsync(u => u.NombreUsuario == request.NombreUsuario && u.IdUsuario != id);
            if (existe)
                return BadRequest(new { message = $"El usuario '{request.NombreUsuario}' ya está en uso." });

            usuario.NombreUsuario = request.NombreUsuario;
        }

        // 2. ACTUALIZAR CONTRASEÑA (Solo si se envía algo)
        if (!string.IsNullOrEmpty(request.Password))
        {
            // IMPORTANTE: Aquí llamamos a tu método de hasheo
            usuario.PasswordHash = HashPassword(request.Password);
        }

        // 3. ACTUALIZAR RESTO DE CAMPOS
        if (request.NombreCompleto != null) usuario.NombreCompleto = request.NombreCompleto;
        if (request.Email != null) usuario.Email = request.Email;
        if (request.IdRol.HasValue) usuario.IdRol = request.IdRol.Value;
        if (request.IdSucursal.HasValue) usuario.IdSucursal = request.IdSucursal;
        if (request.Estatus.HasValue) usuario.Estatus = request.Estatus.Value;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuario actualizado correctamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error interno al actualizar usuario." });
        }
    }

    // --- 4. CAMBIAR ESTATUS (Baja Lógica) ---
    [HttpPatch("{id}/estatus")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CambiarEstatus(int id, [FromBody] UsuarioDtoCambiarEstado request)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        usuario.Estatus = request.Estatus;
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Usuario {(request.Estatus ? "activado" : "desactivado")}." });
    }

    // --- MÉTODO DE HASHEO (Debe ser idéntico al de AuthController) ---
    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password ?? string.Empty));
        return Convert.ToBase64String(bytes);
    }
}