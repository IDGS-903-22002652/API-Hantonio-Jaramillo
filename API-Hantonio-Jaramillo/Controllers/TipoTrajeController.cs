using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Hantonio_Jaramillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoTrajeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipoTrajeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoTraje
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTrajeDtoResponse>>> GetTiposTraje()
        {
            var lista = await _context.TiposTraje
                .Select(t => new TipoTrajeDtoResponse
                {
                    IdTipoTraje = t.IdTipoTraje,
                    Descripcion = t.Descripcion
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}