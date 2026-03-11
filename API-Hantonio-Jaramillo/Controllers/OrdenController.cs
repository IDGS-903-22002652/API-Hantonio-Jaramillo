using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API_Hantonio_Jaramillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // 1. GET: LISTAR TODAS (SOLUCIÓN AL ERROR 404 DEL FRONTEND)
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrdenes()
        {
            // Incluimos las relaciones para que la tabla en React muestre nombres, no solo IDs
            return await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Sucursal)
                .Include(o => o.EstatusOrden)
                .Include(o => o.Medidas)
        .Include(o => o.DetalleSaco)
        .Include(o => o.DetallePantalon)
        .Include(o => o.DetalleChaleco)
        .Include(o => o.DetalleCamisa)

                .OrderByDescending(o => o.IdOrden) // Ordenamos: las nuevas primero
                .ToListAsync();
        }

        // ============================================================
        // 2. POST: CREACIÓN INTEGRAL ("BOTÓN DE ORO")
        // Recibe el objeto maestro y guarda todo en una transacción.
        // ============================================================
        [HttpPost("crear-completa")]
        public async Task<ActionResult> CrearOrdenCompleta([FromBody] OrdenMasterDTOs dto)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Obtenemos el ID del empleado que está logueado (Auditoría)
            if (!int.TryParse(nameIdentifier, out int userId))
            {
                // Opción A: Buscar el ID en la BD usando el nombre "Omar"
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nameIdentifier);
                userId = user?.IdUsuario ?? 1; // Si no existe, usamos 1 como fallback
            }

            // Iniciamos la transacción: Todo se guarda o nada se guarda.
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // A. Crear la Orden Maestra
                var nuevaOrden = new Orden
                {
                    IdCliente = dto.IdCliente,
                    IdUsuario = userId,           // Usuario logueado
                    IdSucursal = dto.IdSucursal,  // Viene del Select del Frontend
                    IdTipoTraje = dto.IdTipoTraje,// Viene de los botones del Frontend
                    IdEstatus = dto.IdEstatus,    // Viene del Select del Frontend

                    FechaCreacion = DateTime.Now,
                    FechaCitaMedidas = dto.FechaCitaMedidas,
                    FechaEventoEntrega = dto.FechaEventoEntrega,

                    // Si manejas costos en este punto (opcional según tu lógica de negocio)
                    CostoTotal = dto.CostoTotal,
                    MontoAbonado = dto.MontoAbonado,
                    MetodoPago = dto.MetodoPago
                };

                _context.Ordenes.Add(nuevaOrden);
                await _context.SaveChangesAsync(); // Aquí se genera el IdOrden autoincrementable

                // B. Mapear y Guardar Medidas (Si existen)
                if (dto.Medidas != null)
                {
                    var m = new MedidasOrden
                    {
                        IdOrden = nuevaOrden.IdOrden, // Vinculamos con la orden recién creada
                        Altura = dto.Medidas.Altura,
                        Peso = dto.Medidas.Peso,
                        TallaZapato = dto.Medidas.TallaZapato,
                        TipoFit = dto.Medidas.TipoFit,
                        // Saco
                        SacoLargoFrente = dto.Medidas.SacoLargoFrente,
                        SacoLargoEspalda = dto.Medidas.SacoLargoEspalda,
                        SacoHombros = dto.Medidas.SacoHombros,
                        SacoPecho = dto.Medidas.SacoPecho,
                        SacoEstomago = dto.Medidas.SacoEstomago,
                        SacoMangaIzq = dto.Medidas.SacoMangaIzq,
                        SacoMangaDer = dto.Medidas.SacoMangaDer,
                        SacoBiceps = dto.Medidas.SacoBiceps,
                        SacoCadera = dto.Medidas.SacoCadera,
                        // Pantalón
                        PantLargoIzq = dto.Medidas.PantLargoIzq,
                        PantLargoDer = dto.Medidas.PantLargoDer,
                        PantCintura = dto.Medidas.PantCintura,
                        PantCadera = dto.Medidas.PantCadera,
                        PantMuslo = dto.Medidas.PantMuslo,
                        PantTiro = dto.Medidas.PantTiro,
                        // Camisa
                        CamisaCuello = dto.Medidas.CamisaCuello,
                        CamisaManga = dto.Medidas.CamisaManga
                    };
                    _context.MedidasOrdenes.Add(m);
                }

                // C. Mapear y Guardar Detalle Saco
                if (dto.Saco != null)
                {
                    var s = new DetalleSaco
                    {
                        IdOrden = nuevaOrden.IdOrden,
                        CodigoTela = dto.Saco.CodigoTela,
                        CodigoForro = dto.Saco.CodigoForro,
                        CodigoBoton = dto.Saco.CodigoBoton,
                        EstiloBotones = dto.Saco.EstiloBotones,
                        EstiloSolapa = dto.Saco.EstiloSolapa,
                        TamanoSolapa = dto.Saco.TamanoSolapa,
                        EstiloBolsilloPecho = dto.Saco.EstiloBolsilloPecho,
                        EstiloBolsilloInf = dto.Saco.EstiloBolsilloInf,
                        EstiloBolsilloTicket = dto.Saco.EstiloBolsilloTicket,
                        EstiloOjalIzquierdo = dto.Saco.EstiloOjalIzquierdo,
                        EstiloOjalDerecho = dto.Saco.EstiloOjalIzquierdo,
                        Monograma = dto.Saco.Monograma,
                        Observaciones = dto.Saco.Observaciones,
                        PrecioSaco = dto.Saco.PrecioSaco
                    };
                    _context.DetalleSacos.Add(s);
                }

                // D. Mapear y Guardar Detalle Pantalón
                if (dto.Pantalon != null)
                {
                    var p = new DetallePantalon
                    {
                        IdOrden = nuevaOrden.IdOrden,
                        CodigoTela = dto.Pantalon.CodigoTela,
                        CodigoBoton = dto.Pantalon.CodigoBoton,
                        EstiloPretina = dto.Pantalon.EstiloPretina,
                        AjusteCintura = dto.Pantalon.AjusteCintura,
                        AlturaPretina = dto.Pantalon.AlturaPretina,
                        EstiloPliegues = dto.Pantalon.EstiloPliegues,
                        EstiloBolsilloReloj = dto.Pantalon.EstiloBolsilloReloj,
                        EstiloBajos = dto.Pantalon.EstiloBajos,
                        Observaciones = dto.Pantalon.Observaciones,
                        PrecioPantalon = dto.Pantalon.PrecioPantalon
                    };
                    _context.DetallePantalones.Add(p);
                }

                // E. Mapear y Guardar Detalle Chaleco
                if (dto.Chaleco != null)
                {
                    var ch = new DetalleChaleco
                    {
                        IdOrden = nuevaOrden.IdOrden,
                        CodigoTela = dto.Chaleco.CodigoTela,
                        CodigoBoton = dto.Chaleco.CodigoBoton,
                        EstiloCuello = dto.Chaleco.EstiloCuello,
                        EstiloBotones = dto.Chaleco.EstiloBotones,
                        EstiloBolsilloPecho = dto.Chaleco.EstiloBolsilloPecho,
                        EstiloBolsilloInf = dto.Chaleco.EstiloBolsilloInf,
                        TerminacionInf = dto.Chaleco.TerminacionInf,
                        Observaciones = dto.Chaleco.Observaciones,
                        PrecioChaleco = dto.Chaleco.PrecioChaleco   
                    };
                    _context.DetalleChalecos.Add(ch);
                }

                // F. Mapear y Guardar Detalle Camisa
                if (dto.Camisa != null)
                {
                    var c = new DetalleCamisa
                    {
                        IdOrden = nuevaOrden.IdOrden,
                        OpcionCamisa = dto.Camisa.OpcionCamisa,
                        CodigoTela = dto.Camisa.CodigoTela,
                        EstiloCuello = dto.Camisa.EstiloCuello,
                        ContrasteTela = dto.Camisa.ContrasteTela,
                        EstiloTapeta = dto.Camisa.EstiloTapeta,
                        EstiloPuno = dto.Camisa.EstiloPuno,
                        EstiloBolsillo = dto.Camisa.EstiloBolsillo,
                        PlieguesFrontales = dto.Camisa.PlieguesFrontales,
                        Iniciales = dto.Camisa.Iniciales,
                        Observaciones = dto.Camisa.Observaciones,
                        PrecioCamisa = dto.Camisa.PrecioCamisa
                    };
                    _context.DetalleCamisas.Add(c);
                }

                // Guardamos todos los detalles y confirmamos la transacción
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Orden integral creada con éxito", id = nuevaOrden.IdOrden });
            }
            catch (Exception ex)
            {
                // Si algo falla, deshacemos cualquier cambio en la BD para no dejar basura
                await transaction.RollbackAsync();

                // Tip: En producción, loguea 'ex' internamente y no devuelvas ex.Message al cliente por seguridad.
                // Por ahora para desarrollo está bien.
                return BadRequest($"Error al procesar la orden: {ex.Message} {ex.InnerException?.Message}");
            }
        }

        // ============================================================
        // MÉTODOS ADICIONALES (Filtros, Delete, etc.)
        // ============================================================

        [HttpGet("sucursal/{idSucursal}")]
        public async Task<ActionResult<IEnumerable<Orden>>> GetBySucursal(int idSucursal)
        {
            return await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.EstatusOrden)
                .Include(o => o.Medidas)
                .Include(o => o.DetalleSaco)
                .Include(o => o.DetallePantalon)
                .Include(o => o.DetalleChaleco)
                .Include(o => o.DetalleCamisa)
                .Where(o => o.IdSucursal == idSucursal)
                .OrderByDescending(o => o.FechaCreacion)
                .ToListAsync();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")] // Solo admin puede borrar
        public async Task<IActionResult> DeleteOrden(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden == null) return NotFound();

            // Al borrar la orden, EF Core borrará los detalles en cascada
            // si la BD está configurada con ON DELETE CASCADE
            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Expediente eliminado correctamente." });
        }
    }
}