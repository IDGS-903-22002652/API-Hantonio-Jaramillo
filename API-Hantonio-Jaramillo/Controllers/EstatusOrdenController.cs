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
    [Authorize]
    public class EstatusOrdenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstatusOrdenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EstatusOrden
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstatusOrdenDtoResponse>>> GetEstatus()
        {
            var lista = await _context.EstatusOrdenes
                .Select(e => new EstatusOrdenDtoResponse
                {
                    IdEstatus = e.IdEstatus,
                    Descripcion = e.Descripcion
                })
                .ToListAsync();

            return Ok(lista);
        }
    }
}