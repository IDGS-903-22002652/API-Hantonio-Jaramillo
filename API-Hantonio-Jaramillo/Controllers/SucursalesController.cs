using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SucursalesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SucursalesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
    {
        return await _context.Sucursales.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sucursal>> GetSucursal(int id)
    {
        var sucursal = await _context.Sucursales.FindAsync(id);
        return sucursal is null ? NotFound() : sucursal;
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<Sucursal>> CreateSucursal(Sucursal sucursal)
    {
        if (string.IsNullOrWhiteSpace(sucursal.Nombre))
        {
            return BadRequest("El nombre de la sucursal es obligatorio.");
        }

        _context.Sucursales.Add(sucursal);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.IdSucursal }, sucursal);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> UpdateSucursal(int id, Sucursal sucursal)
    {
        if (id != sucursal.IdSucursal)
        {
            return BadRequest("El ID no coincide.");
        }

        if (!await _context.Sucursales.AnyAsync(s => s.IdSucursal == id))
        {
            return NotFound();
        }

        _context.Entry(sucursal).State = EntityState.Modified;

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
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteSucursal(int id)
    {
        var sucursal = await _context.Sucursales.FindAsync(id);
        if (sucursal is null) return NotFound();

        _context.Sucursales.Remove(sucursal);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}