
using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        // Retornar TODOS los clientes (activos e inactivos)
        return await _context.Clientes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        // Ignorar filtros globales para poder obtener clientes inactivos cuando sea necesario
        var cliente = await _context.Clientes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.IdCliente == id);

        return cliente is null ? NotFound() : cliente;
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.NombreCompleto))
        {
            return BadRequest("El nombre completo es obligatorio.");
        }

        cliente.FechaRegistro = DateTime.UtcNow;
        cliente.Activo = true;
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(int id, Cliente model)
    {
        var entidad = await _context.Clientes.FindAsync(id);
        if (entidad is null) return NotFound();

        // Actualizar todos los campos
        entidad.NombreCompleto = model.NombreCompleto;
        entidad.Telefono = model.Telefono;
        entidad.Email = model.Email;
        entidad.Ciudad = model.Ciudad;
        entidad.Estado = model.Estado;
        entidad.Activo = model.Activo; // ← Permite reactivar

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente is null) return NotFound();

        // Eliminación lógica: marcar como inactivo
        cliente.Activo = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }

}