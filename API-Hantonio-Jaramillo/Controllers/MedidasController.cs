using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere que el sastre esté logueado
    public class MedidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. OBTENER MEDIDAS POR ID DE ORDEN ---
        // Útil para cargar el formulario de edición en React
        [HttpGet("orden/{idOrden}")]
        public async Task<ActionResult<MedidasOrden>> GetByOrden(int idOrden)
        {
            var medidas = await _context.MedidasOrdenes
                .FirstOrDefaultAsync(m => m.IdOrden == idOrden);

            if (medidas == null)
            {
                return NotFound(new { message = "No se encontraron medidas registradas para esta orden." });
            }

            return Ok(medidas);
        }

        // --- 2. ACTUALIZAR MEDIDAS ---
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedidas(int id, [FromBody] MedidasOrden medidas)
        {
            if (id != medidas.IdMedida)
            {
                return BadRequest("El ID de la medida no coincide con el registro.");
            }

            // Marcamos el registro como modificado
            _context.Entry(medidas).State = EntityState.Modified;

            // Protegemos el IdOrden para que no se pueda cambiar accidentalmente a otra orden
            _context.Entry(medidas).Property(x => x.IdOrden).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedidasExists(id)) return NotFound();
                else throw;
            }

            return Ok(new { message = "Las medidas se han actualizado correctamente en el expediente." });
        }

        // --- 3. OBTENER UNA MEDIDA ESPECÍFICA POR ID ---
        [HttpGet("{id}")]
        public async Task<ActionResult<MedidasOrden>> GetMedida(int id)
        {
            var medida = await _context.MedidasOrdenes.FindAsync(id);

            if (medida == null) return NotFound();

            return medida;
        }

        private bool MedidasExists(int id) => _context.MedidasOrdenes.Any(e => e.IdMedida == id);
    }
}