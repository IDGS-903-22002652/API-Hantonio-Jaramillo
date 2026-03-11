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

        // 1. OBTENER CLIENTES (Solo los activos por defecto)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] string? buscar, [FromQuery] bool soloActivos = true)
        {
            var query = _context.Clientes.AsQueryable();

            // Filtrar por estatus si se solicita
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

        // 2. OBTENER POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound("El cliente no existe.");
            return cliente;
        }

        // 3. REGISTRAR (Usando tus valores por defecto)
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
        {
            // La FechaRegistro y Estatus ya tienen valores default en tu modelo
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        // 4. ACTUALIZAR (Incluyendo Ciudad y Estado)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest("El ID no coincide.");

            _context.Entry(cliente).State = EntityState.Modified;

            // Evitamos que se modifique la fecha de registro original
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

        // 5. BAJA LÓGICA (Cambiar Estatus en lugar de eliminar físicamente)
        [HttpPatch("{id}/estatus")]
        [Authorize(Roles = "Administrador")]
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