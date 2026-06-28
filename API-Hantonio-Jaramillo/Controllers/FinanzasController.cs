using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.Models;
using API_Hantonio_Jaramillo.DTOs;
using Microsoft.Extensions.Logging;

namespace API_Hantonio_Jaramillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanzasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FinanzasController> _logger;

        public FinanzasController(ApplicationDbContext context, ILogger<FinanzasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/Finanzas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFinanzas()
        {
            try
            {
                var historial = await _context.Finanzas
                    .OrderByDescending(f => f.Fecha)
                    .ToListAsync();

                return Ok(historial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener finanzas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// 🔥 GET: api/Finanzas/reporte
        /// </summary>
        [HttpGet("reporte")]
        public async Task<IActionResult> ObtenerReporte()
        {
            try
            {
                var finanzas = await _context.Finanzas.ToListAsync();

                if (!finanzas.Any())
                {
                    return Ok(new
                    {
                        totalVentas = 0,
                        totalInversion = 0,
                        promedioROI = 0
                    });
                }

                var totalVentas = finanzas.Sum(f => f.GananciaNeta);
                var totalInversion = finanzas.Sum(f => f.CostoInversion);
                var promedioROI = finanzas.Average(f => f.ROI);

                return Ok(new
                {
                    totalVentas,
                    totalInversion,
                    promedioROI
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar reporte");
                return StatusCode(500, "Error al generar reporte");
            }
        }

        /// <summary>
        /// POST: api/Finanzas
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearFinanza([FromBody] FinanzaCreateDTO finanzaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(kvp => kvp.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    _logger.LogWarning("Error de validación: {@Errors}", errors);

                    return BadRequest(new { message = "Errores de validación", errors });
                }

                var nuevaFinanza = new Finanza
                {
                    Descripcion = finanzaDto.Descripcion,
                    GananciaNeta = finanzaDto.GananciaNeta,
                    CostoInversion = finanzaDto.CostoInversion,
                    ROI = finanzaDto.ROI,
                    Fecha = DateTime.UtcNow
                };

                _context.Finanzas.Add(nuevaFinanza);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFinanzas), new { id = nuevaFinanza.Id }, nuevaFinanza);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear finanza");
                return StatusCode(500, "Error al guardar el cálculo");
            }
        }
    }
}
