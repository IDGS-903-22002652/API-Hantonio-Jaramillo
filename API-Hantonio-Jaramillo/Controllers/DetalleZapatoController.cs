using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DetalleZapatoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DetalleZapatoController(ApplicationDbContext context) => _context = context;

        [HttpGet("orden/{idOrden}")]
        public async Task<ActionResult<DetalleZapato>> GetByOrden(int idOrden)
        {
            var detalle = await _context.DetalleZapatos.FirstOrDefaultAsync(d => d.IdOrden == idOrden);
            return detalle == null ? NotFound() : detalle;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DetalleZapato detalle)
        {
            if (id != detalle.IdDetalleZapato) return BadRequest();

            _context.Entry(detalle).State = EntityState.Modified;
            _context.Entry(detalle).Property(x => x.IdOrden).IsModified = false; // Agregado: Protege la relación

            await _context.SaveChangesAsync();
            return Ok(new { message = "Detalle de los zapatos actualizado." });
        }
    }
}
