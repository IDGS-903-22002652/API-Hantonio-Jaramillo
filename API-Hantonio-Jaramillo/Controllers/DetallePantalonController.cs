using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DetallePantalonController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DetallePantalonController(ApplicationDbContext context) => _context = context;

    [HttpGet("orden/{idOrden}")]
    public async Task<ActionResult<DetallePantalon>> GetByOrden(int idOrden)
    {
        var detalle = await _context.DetallePantalones.FirstOrDefaultAsync(d => d.IdOrden == idOrden);
        return detalle == null ? NotFound() : detalle;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DetallePantalon detalle)
    {
        if (id != detalle.IdDetallePantalon) return BadRequest();

        _context.Entry(detalle).State = EntityState.Modified;
        _context.Entry(detalle).Property(x => x.IdOrden).IsModified = false; // Agregado: Protege la relación

        await _context.SaveChangesAsync();
        return Ok(new { message = "Detalle del pantalón actualizado." });
    }
}