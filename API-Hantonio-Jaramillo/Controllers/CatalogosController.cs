using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CatalogosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CatalogosController(AppDbContext context)
    {
        _context = context;
    }

    // CAT_ESTATUS
    [HttpGet("estatus")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CatEstatus>>> GetEstatus()
    {
        return await _context.CatEstatus.ToListAsync();
    }

    [HttpPost("estatus")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<CatEstatus>> CreateEstatus(CatEstatus estatus)
    {
        if (string.IsNullOrWhiteSpace(estatus.Nombre))
        {
            return BadRequest("El nombre del estatus es obligatorio.");
        }

        _context.CatEstatus.Add(estatus);
        await _context.SaveChangesAsync();
        return Ok(estatus);
    }

    // CAT_TIPO_TRAJE
    [HttpGet("tipos-traje")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CatTipoTraje>>> GetTiposTraje()
    {
        return await _context.CatTipoTrajes.ToListAsync();
    }

    [HttpPost("tipos-traje")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<CatTipoTraje>> CreateTipoTraje(CatTipoTraje tipoTraje)
    {
        if (string.IsNullOrWhiteSpace(tipoTraje.Nombre))
        {
            return BadRequest("El nombre del tipo de traje es obligatorio.");
        }

        _context.CatTipoTrajes.Add(tipoTraje);
        await _context.SaveChangesAsync();
        return Ok(tipoTraje);
    }

    // CAT_RECURSOS_DISENO
    [HttpGet("recursos-diseno")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CatRecursosDiseno>>> GetRecursosDiseno()
    {
        return await _context.CatRecursosDiseno.ToListAsync();
    }

    [HttpGet("recursos-diseno/{prenda}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<CatRecursosDiseno>>> GetRecursosByPrenda(string prenda)
    {
        return await _context.CatRecursosDiseno
            .Where(r => r.Prenda.ToUpper() == prenda.ToUpper())
            .ToListAsync();
    }

    [HttpPost("recursos-diseno")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<CatRecursosDiseno>> CreateRecursoDiseno(CatRecursosDiseno recurso)
    {
        if (string.IsNullOrWhiteSpace(recurso.Prenda))
        {
            return BadRequest("La prenda es obligatoria.");
        }

        _context.CatRecursosDiseno.Add(recurso);
        await _context.SaveChangesAsync();
        return Ok(recurso);
    }
}