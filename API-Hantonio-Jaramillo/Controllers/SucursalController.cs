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
    // 1. Autorización general: Cualquier usuario con token válido (Admin o Empleado) puede entrar al controlador
    [Authorize]
    public class SucursalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SucursalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. LISTAR SUCURSALES ---
        // Al no ponerle un rol específico, hereda el [Authorize] de la clase.
        // Esto permite que el Empleado pueda cargar la lista para el Select de Órdenes.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
        {
            return await _context.Sucursales
                .Include(s => s.Usuario)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }

        // --- 2. OBTENER POR ID ---
        // Igual que el anterior, abierto a ambos roles.
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursales
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.IdSucursal == id);

            if (sucursal == null) return NotFound("Sucursal no encontrada.");

            return sucursal;
        }

        // --- 3. AGREGAR NUEVA SUCURSAL ---
        [HttpPost]
        // 2. Candado específico: Solo Administrador
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Sucursal>> PostSucursal([FromBody] Sucursal sucursal)
        {
            sucursal.Estatus = true;

            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.IdSucursal }, sucursal);
        }

        // --- 4. ACTUALIZAR SUCURSAL ---
        [HttpPut("{id}")]
        // 3. Candado específico: Solo Administrador
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutSucursal(int id, [FromBody] Sucursal sucursal)
        {
            if (id != sucursal.IdSucursal) return BadRequest("El ID no coincide.");

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id)) return NotFound();
                else throw;
            }

            return Ok(new { message = "Sucursal actualizada correctamente." });
        }

        // --- 5. CAMBIAR ESTATUS (Baja Lógica) ---
        [HttpPatch("{id}/estatus")]
        // 4. Candado específico: Solo Administrador
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CambiarEstatus(int id, [FromBody] bool nuevoEstatus)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null) return NotFound();

            sucursal.Estatus = nuevoEstatus;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Sucursal {(nuevoEstatus ? "activada" : "desactivada")} correctamente." });
        }

        private bool SucursalExists(int id) => _context.Sucursales.Any(e => e.IdSucursal == id);
    }
}