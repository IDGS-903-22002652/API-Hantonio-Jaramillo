using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DetalleSacoController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DetalleSacoController(ApplicationDbContext context) => _context = context;

    [HttpGet("orden/{idOrden}")]
    public async Task<ActionResult<DetalleSaco>> GetByOrden(int idOrden)
    {
        var detalle = await _context.DetalleSacos.FirstOrDefaultAsync(d => d.IdOrden == idOrden);
        return detalle == null ? NotFound() : detalle;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DetalleSaco detalle)
    {
        if (id != detalle.IdDetalleSaco) return BadRequest();
        _context.Entry(detalle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Diseño del saco actualizado" });
    }
}