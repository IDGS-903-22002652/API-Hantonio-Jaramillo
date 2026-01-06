using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers;

[Authorize(Roles = "Administrador")]
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly AppDbContext _context;

    public RolesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rol>> GetRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        return rol is null ? NotFound() : rol;
    }

    [HttpPost]
    public async Task<ActionResult<Rol>> CreateRol(Rol rol)
    {
        if (string.IsNullOrWhiteSpace(rol.Nombre))
        {
            return BadRequest("El nombre del rol es obligatorio.");
        }

        if (await _context.Roles.AnyAsync(r => r.Nombre == rol.Nombre))
        {
            return Conflict($"Ya existe un rol con el nombre '{rol.Nombre}'.");
        }

        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRol), new { id = rol.IdRol }, rol);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRol(int id, Rol rol)
    {
        if (id != rol.IdRol)
        {
            return BadRequest("El ID no coincide.");
        }

        if (!await _context.Roles.AnyAsync(r => r.IdRol == id))
        {
            return NotFound();
        }

        _context.Entry(rol).State = EntityState.Modified;

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol is null) return NotFound();

        // Verificar que no hay usuarios con este rol
        if (await _context.Usuarios.AnyAsync(u => u.IdRol == id))
        {
            return Conflict("No se puede eliminar el rol porque tiene usuarios asignados.");
        }

        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}