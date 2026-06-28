using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] string? buscar, [FromQuery] bool soloActivos = true)
        {
            var query = _context.Clientes.AsQueryable();

            if (soloActivos)
            {
                query = query.Where(c => c.Estatus);
            }

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                query = query.Where(c => c.NombreCompleto.Contains(buscar) ||
                                         c.Telefono.Contains(buscar) ||
                                         c.Ciudad.Contains(buscar));
            }

            return await query.OrderByDescending(c => c.FechaRegistro).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound("El cliente no existe.");
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest("El ID no coincide.");
            _context.Entry(cliente).State = EntityState.Modified;
            _context.Entry(cliente).Property(x => x.FechaRegistro).IsModified = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id)) return NotFound();
                else throw;
            }
            return Ok(new { message = "Datos del cliente actualizados correctamente." });
        }

        [HttpPatch("{id}/estatus")]
        public async Task<IActionResult> CambiarEstatus(int id, [FromBody] bool nuevoEstatus)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            cliente.Estatus = nuevoEstatus;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Cliente {(nuevoEstatus ? "activado" : "desactivado")} correctamente." });
        }

        private bool ClienteExists(int id) => _context.Clientes.Any(e => e.IdCliente == id);
    }
}