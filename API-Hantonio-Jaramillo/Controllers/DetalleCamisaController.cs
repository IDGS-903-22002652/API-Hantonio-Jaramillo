using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DetalleCamisaController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DetalleCamisaController(ApplicationDbContext context) => _context = context;

    [HttpGet("orden/{idOrden}")]
    public async Task<ActionResult<DetalleCamisa>> GetByOrden(int idOrden)
    {
        var detalle = await _context.DetalleCamisas.FirstOrDefaultAsync(d => d.IdOrden == idOrden);
        return detalle == null ? NotFound() : detalle;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DetalleCamisa detalle)
    {
        if (id != detalle.IdDetalleCamisa) return BadRequest();
        _context.Entry(detalle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Detalle de la camisa actualizado." });
    }
}