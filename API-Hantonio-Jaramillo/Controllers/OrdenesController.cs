using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdenesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdenesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Orden>>> GetOrdenes()
    {
        return await _context.Ordenes
            .Include(o => o.Cliente)
            .Include(o => o.Estatus)
            .Include(o => o.TipoTraje)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Orden>> GetOrden(int id)
    {
        var orden = await _context.Ordenes
            .Include(o => o.Cliente)
            .Include(o => o.Estatus)
            .Include(o => o.TipoTraje)
            .Include(o => o.MedidasOrden)
            .Include(o => o.DetalleSaco)
            .Include(o => o.DetalleCamisa)
            .FirstOrDefaultAsync(o => o.IdOrden == id);

        return orden is null ? NotFound() : orden;
    }

    [HttpPost]
    public async Task<ActionResult<Orden>> CreateOrden(Orden orden)
    {
        // Validar cliente existe
        if (!await _context.Clientes.AnyAsync(c => c.IdCliente == orden.IdCliente))
        {
            return BadRequest($"El cliente con ID '{orden.IdCliente}' no existe.");
        }

        // Validar estatus existe
        if (!await _context.CatEstatus.AnyAsync(e => e.IdEstatus == orden.IdEstatus))
        {
            return BadRequest($"El estatus con ID '{orden.IdEstatus}' no existe.");
        }

        // Validar tipo traje existe
        if (!await _context.CatTipoTrajes.AnyAsync(t => t.IdTipoTraje == orden.IdTipoTraje))
        {
            return BadRequest($"El tipo de traje con ID '{orden.IdTipoTraje}' no existe.");
        }

        orden.FechaCreacion = DateTime.UtcNow;
        _context.Ordenes.Add(orden);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrden), new { id = orden.IdOrden }, orden);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrden(int id, Orden orden)
    {
        if (id != orden.IdOrden)
        {
            return BadRequest("El ID no coincide.");
        }

        if (!await _context.Ordenes.AnyAsync(o => o.IdOrden == id))
        {
            return NotFound();
        }

        _context.Entry(orden).State = EntityState.Modified;

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
    public async Task<IActionResult> DeleteOrden(int id)
    {
        var orden = await _context.Ordenes.FindAsync(id);
        if (orden is null) return NotFound();

        _context.Ordenes.Remove(orden);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}