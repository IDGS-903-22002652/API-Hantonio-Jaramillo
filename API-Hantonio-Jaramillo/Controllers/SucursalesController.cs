using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SucursalesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SucursalesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<Sucursal>>> GetSucursales()
    {
        // Retornar TODAS las sucursales (activas e inactivas)
        var list = await _context.Sucursales.ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Sucursal>> CreateSucursal([FromBody] Sucursal model)
    {
        _context.Sucursales.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSucursales), new { id = model.IdSucursal }, model);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateSucursal(int id, [FromBody] Sucursal model)
    {
        var entidad = await _context.Sucursales.FindAsync(id);
        if (entidad == null) return NotFound();

        // ✅ ACTUALIZAR TODOS LOS CAMPOS
        entidad.Nombre = model.Nombre;
        entidad.Direccion = model.Direccion;
        entidad.Telefono = model.Telefono;
        entidad.Encargado = model.Encargado;
        entidad.Activa = model.Activa;  // ← ESTE ES EL CAMPO CRÍTICO

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSucursal(int id)
    {
        var entidad = await _context.Sucursales.FindAsync(id);
        if (entidad == null) return NotFound();

        // Eliminación lógica
        entidad.Activa = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
