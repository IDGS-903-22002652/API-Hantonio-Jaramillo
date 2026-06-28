using API_Hantonio_Jaramillo.Data;
using API_Hantonio_Jaramillo.DTOs;
using API_Hantonio_Jaramillo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrdenes()
        {
            return await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Sucursal)
                .Include(o => o.EstatusOrden)
                .Include(o => o.Medidas)
        .Include(o => o.DetalleSaco)
        .Include(o => o.DetallePantalon)
        .Include(o => o.DetalleChaleco)
        .Include(o => o.DetalleCamisa)
        .Include(o => o.DetalleZapato)
                .OrderByDescending(o => o.IdOrden) 
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orden>> GetOrdenById(int id)
        {
            var orden = await _context.Ordenes
                .Include(o => o.Cliente)
                .Include(o => o.Sucursal)
                .Include(o => o.EstatusOrden)
                .Include(o => o.Medidas)
                .Include(o => o.DetalleSaco)
                .Include(o => o.DetallePantalon)
                .Include(o => o.DetalleChaleco)
                .Include(o => o.DetalleCamisa)
                        .Include(o => o.DetalleZapato)

                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (orden == null) return NotFound(new { message = "Orden no encontrada" });

            return Ok(orden);
        }


        [HttpPost("crear-completa")]
        public async Task<ActionResult> CrearOrdenCompleta([FromBody] OrdenDTOs dto)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(nameIdentifier, out int userId))
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nameIdentifier);
                userId = user?.IdUsuario ?? 1;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevaOrden = new Orden
                {
                    IdCliente = dto.IdCliente,
                    IdUsuario = userId,           
                    IdSucursal = dto.IdSucursal,  
                    IdTipoTraje = dto.IdTipoTraje ,
                    IncluyeCamisa = dto.IncluyeCamisa,
                    IncluyeZapato = dto.IncluyeZapato,
                    esSmoking3Piezas = dto.esSmoking3Piezas,
                    IdEstatus = dto.IdEstatus,    

                    FechaCreacion = DateTime.Now,
                    FechaCitaMedidas = dto.FechaCitaMedidas,
                    FechaEntrega = dto.FechaEntrega,
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
                        IdOrden = nuevaOrden.IdOrden, // Vinculación vital

                        // --- DATOS GENERALES ---
                        Altura = dto.Medidas.Altura,
                        Peso = dto.Medidas.Peso,
                        TipoFit = dto.Medidas.TipoFit,

                        // --- MEDIDAS DE SACO ---
                        CollarSaco = dto.Medidas.CollarSaco,
                        LongitudFrontalSaco = dto.Medidas.LongitudFrontalSaco,
                        LongitudEspaldaSaco = dto.Medidas.LongitudEspaldaSaco,
                        HombrosSaco = dto.Medidas.HombrosSaco,
                        PechoSaco = dto.Medidas.PechoSaco,
                        PechoDelanteroSaco = dto.Medidas.PechoDelanteroSaco,
                        EstomagoSaco = dto.Medidas.EstomagoSaco,
                        VientreSaco = dto.Medidas.VientreSaco,
                        CaderasSaco = dto.Medidas.CaderasSaco,
                        LongitudMangaISaco = dto.Medidas.LongitudMangaISaco,
                        LongitudMangaDSaco = dto.Medidas.LongitudMangaDSaco,
                        BicepsSaco = dto.Medidas.BicepsSaco,
                        AntebrazoSaco = dto.Medidas.AntebrazoSaco,
                        MuñecaSaco = dto.Medidas.MuñecaSaco,
                        HombroDelanteroSaco = dto.Medidas.HombroDelanteroSaco,
                        AnchoTraseroSaco = dto.Medidas.AnchoTraseroSaco,
                        NucaCinturaSaco = dto.Medidas.NucaCinturaSaco,
                        LongitudCinturaDelantera = dto.Medidas.LongitudCinturaDelantera,
                        PosicionPrimerBSaco = dto.Medidas.PosicionPrimerBSaco,

                        // --- MEDIDAS DE CAMISA ---
                        CollarCamisa = dto.Medidas.CollarCamisa,
                        LongitudFrontalCamisa = dto.Medidas.LongitudFrontalCamisa,
                        LongitudEspaldaCamisa = dto.Medidas.LongitudEspaldaCamisa,
                        HombrosCamisa = dto.Medidas.HombrosCamisa,
                        PechoCamisa = dto.Medidas.PechoCamisa,
                        PechoDelanteroCamisa = dto.Medidas.PechoDelanteroCamisa,
                        EstomagoCamisa = dto.Medidas.EstomagoCamisa,
                        VientreCamisa = dto.Medidas.VientreCamisa,
                        CaderasCamisa = dto.Medidas.CaderasCamisa,
                        LongitudMangaICamisa = dto.Medidas.LongitudMangaICamisa,
                        LongitudMangaDCamisa = dto.Medidas.LongitudMangaDCamisa,
                        BicepsCamisa = dto.Medidas.BicepsCamisa,
                        AntebrazoCamisa = dto.Medidas.AntebrazoCamisa,
                        MuñecaCamisa = dto.Medidas.MuñecaCamisa,
                        HombroDelanteroCamisa = dto.Medidas.HombroDelanteroCamisa,
                        AnchoTraseroCamisa = dto.Medidas.AnchoTraseroCamisa,
                        NucaCinturaCamisa = dto.Medidas.NucaCinturaCamisa,
                        LongitudCinturaDelanteraCamisa = dto.Medidas.LongitudCinturaDelanteraCamisa,
                        PosicionPrimerBCamisa = dto.Medidas.PosicionPrimerBCamisa,

                        // --- MEDIDAS DE PANTALÓN ---
                        LongitudIPantalon = dto.Medidas.LongitudIPantalon,
                        LongitudDPantalon = dto.Medidas.LongitudDPantalon,
                        CinturaPantalon = dto.Medidas.CinturaPantalon,
                        CaderaPantalon = dto.Medidas.CaderaPantalon,
                        MusloPantalon = dto.Medidas.MusloPantalon,
                        RodillaPantalon = dto.Medidas.RodillaPantalon,
                        AlTerrillaPantalon = dto.Medidas.AlTerrillaPantalon,
                        BrazaletePantalon = dto.Medidas.BrazaletePantalon,
                        EntrepiernaPantalon = dto.Medidas.EntrepiernaPantalon,
                        AlturaCinturaTPantalon = dto.Medidas.AlturaCinturaTPantalon,
                        AlturaCinturaDPantalon = dto.Medidas.AlturaCinturaDPantalon,

                        // --- MEDIDAS DE CHALECO ---
                        CollarChaleco = dto.Medidas.CollarChaleco,
                        LongitudFrontalChaleco = dto.Medidas.LongitudFrontalChaleco,
                        LongitudEspaldaChaleco = dto.Medidas.LongitudEspaldaChaleco,
                        PechoChaleco = dto.Medidas.PechoChaleco,
                        PechoDelanteroChaleco = dto.Medidas.PechoDelanteroChaleco,
                        EstomagoChaleco = dto.Medidas.EstomagoChaleco,
                        VientreChaleco = dto.Medidas.VientreChaleco,
                        CaderasChaleco = dto.Medidas.CaderasChaleco,
                        TamañoInferiorChaleco = dto.Medidas.TamañoInferiorChaleco,
                        LongitudCinturaDChaleco = dto.Medidas.LongitudCinturaDChaleco,
                        NucaCinturaChaleco = dto.Medidas.NucaCinturaChaleco,
                        PosicionPrimerBChaleco = dto.Medidas.PosicionPrimerBChaleco,

                        TallaZapato= dto.Medidas.TallaZapato,
                        AnchoEmpeineZapato = dto.Medidas.AnchoEmpeineZapato,
                        LargoPieZapato = dto.Medidas.LargoPieZapato
                    };

                    _context.MedidasOrdenes.Add(m);
                }

                // C. Mapear y Guardar Detalle Saco
                if (dto.Saco != null)
                {
                    var s = new DetalleSaco
                    {
                        IdOrden = nuevaOrden.IdOrden,
                        NumeroProduccion = dto.Saco.NumeroProduccion,
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
                        EstiloOjalDerecho = dto.Saco.EstiloOjalDerecho,
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
                        NumeroProduccion = dto.Pantalon.NumeroProduccion,
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
                        NumeroProduccion = dto.Chaleco.NumeroProduccion,
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
                        NumeroProduccion = dto.Camisa.NumeroProduccion,
                        OpcionCamisa = dto.Camisa.OpcionCamisa,
                        CodigoTela = dto.Camisa.CodigoTela,
                        EstiloCuello = dto.Camisa.EstiloCuello,
                        ContrasteTela = dto.Camisa.ContrasteTela,
                        EstiloTapeta = dto.Camisa.EstiloTapeta,
                        EstiloPuno = dto.Camisa.EstiloPuno,
                        EstiloBolsillo = dto.Camisa.EstiloBolsillo,
                        PlieguesFrontales = dto.Camisa.PlieguesFrontales,
                        SolapaBolsillo = dto.Camisa.SolapaBolsillo,
                        PosicionContraste = dto.Camisa.PosicionContraste,
                        Iniciales = dto.Camisa.Iniciales,
                        Observaciones = dto.Camisa.Observaciones,
                        PrecioCamisa = dto.Camisa.PrecioCamisa
                    };
                    _context.DetalleCamisas.Add(c);
                }
                if (dto.Zapato != null)
                {
                    var z = new DetalleZapato
                    {
                        IdOrden = nuevaOrden.IdOrden,
                        NumeroProduccion =  dto.Zapato.NumeroProduccion,
                        EstiloZapato = dto.Zapato.EstiloZapato,
                        Observaciones = dto.Zapato.Observaciones,
                        PrecioZapato = dto.Zapato.PrecioZapato
                    };
                    _context.DetalleZapatos.Add(z);
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
        [HttpPut("actualizar-completa/{id}")]
        public async Task<ActionResult> ActualizarOrdenCompleta(int id, [FromBody] OrdenDTOs dto)
        {
            var ordenExistente = await _context.Ordenes
                .Include(o => o.Medidas)
                .Include(o => o.DetalleSaco)
                .Include(o => o.DetallePantalon)
                .Include(o => o.DetalleChaleco)
                .Include(o => o.DetalleCamisa)
                                .Include(o => o.DetalleZapato)
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (ordenExistente == null) return NotFound(new { message = "Orden no encontrada" });

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Actualizar Datos Generales de la Orden
                ordenExistente.IdCliente = dto.IdCliente;
                ordenExistente.IdSucursal = dto.IdSucursal;
                ordenExistente.IdTipoTraje = dto.IdTipoTraje;
                ordenExistente.IncluyeCamisa = dto.IncluyeCamisa;
                ordenExistente.IncluyeZapato = dto.IncluyeZapato;
                ordenExistente.esSmoking3Piezas = dto.esSmoking3Piezas;
                ordenExistente.IdEstatus = dto.IdEstatus;
                ordenExistente.FechaCitaMedidas = dto.FechaCitaMedidas;
                ordenExistente.FechaEntrega = dto.FechaEntrega;
                ordenExistente.FechaEventoEntrega = dto.FechaEventoEntrega;
                ordenExistente.CostoTotal = dto.CostoTotal;
                ordenExistente.MontoAbonado = dto.MontoAbonado;
                ordenExistente.MetodoPago = dto.MetodoPago;

                // 2. Medidas (Actualizar o Crear)
                if (dto.Medidas != null)
                {
                    if (ordenExistente.Medidas != null)
                    {
                        _context.Entry(ordenExistente.Medidas).CurrentValues.SetValues(dto.Medidas);
                        _context.Entry(ordenExistente.Medidas).Property(x => x.IdMedida).IsModified = false;
                        _context.Entry(ordenExistente.Medidas).Property(x => x.IdOrden).IsModified = false;
                    }
                    else
                    {
                        var nuevasMedidas = new MedidasOrden { IdOrden = id };
                        _context.Entry(nuevasMedidas).CurrentValues.SetValues(dto.Medidas);
                        _context.MedidasOrdenes.Add(nuevasMedidas);
                    }
                }

                // 3. Detalle Saco
                if (dto.Saco != null)
                {
                    if (ordenExistente.DetalleSaco != null)
                    {
                        _context.Entry(ordenExistente.DetalleSaco).CurrentValues.SetValues(dto.Saco);
                        _context.Entry(ordenExistente.DetalleSaco).Property(x => x.IdDetalleSaco).IsModified = false;
                        _context.Entry(ordenExistente.DetalleSaco).Property(x => x.IdOrden).IsModified = false;
                    }
                    else
                    {
                        var nuevoSaco = new DetalleSaco { IdOrden = id };
                        _context.Entry(nuevoSaco).CurrentValues.SetValues(dto.Saco);
                        _context.DetalleSacos.Add(nuevoSaco);
                    }
                }
                else if (ordenExistente.DetalleSaco != null) _context.DetalleSacos.Remove(ordenExistente.DetalleSaco);

                // 4. Detalle Pantalón
                if (dto.Pantalon != null)
                {
                    if (ordenExistente.DetallePantalon != null)
                    {
                        _context.Entry(ordenExistente.DetallePantalon).CurrentValues.SetValues(dto.Pantalon);
                        _context.Entry(ordenExistente.DetallePantalon).Property(x => x.IdDetallePantalon).IsModified = false;
                        _context.Entry(ordenExistente.DetallePantalon).Property(x => x.IdOrden).IsModified = false;
                    }
                    else
                    {
                        var nuevoPantalon = new DetallePantalon { IdOrden = id };
                        _context.Entry(nuevoPantalon).CurrentValues.SetValues(dto.Pantalon);
                        _context.DetallePantalones.Add(nuevoPantalon);
                    }
                }
                else if (ordenExistente.DetallePantalon != null) _context.DetallePantalones.Remove(ordenExistente.DetallePantalon);

                // 5. Detalle Chaleco
                if (dto.Chaleco != null)
                {
                    if (ordenExistente.DetalleChaleco != null)
                    {
                        _context.Entry(ordenExistente.DetalleChaleco).CurrentValues.SetValues(dto.Chaleco);
                        _context.Entry(ordenExistente.DetalleChaleco).Property(x => x.IdDetalleChaleco).IsModified = false;
                        _context.Entry(ordenExistente.DetalleChaleco).Property(x => x.IdOrden).IsModified = false;
                    }
                    else
                    {
                        var nuevoChaleco = new DetalleChaleco { IdOrden = id };
                        _context.Entry(nuevoChaleco).CurrentValues.SetValues(dto.Chaleco);
                        _context.DetalleChalecos.Add(nuevoChaleco);
                    }
                }
                else if (ordenExistente.DetalleChaleco != null) _context.DetalleChalecos.Remove(ordenExistente.DetalleChaleco);

                // 6. Detalle Camisa
                if (dto.Camisa != null)
                {
                    if (ordenExistente.DetalleCamisa != null)
                    {
                        _context.Entry(ordenExistente.DetalleCamisa).CurrentValues.SetValues(dto.Camisa);
                        _context.Entry(ordenExistente.DetalleCamisa).Property(x => x.IdDetalleCamisa).IsModified = false;
                        _context.Entry(ordenExistente.DetalleCamisa).Property(x => x.IdOrden).IsModified = false;
                    }
                    else
                    {
                        var nuevaCamisa = new DetalleCamisa { IdOrden = id };
                        _context.Entry(nuevaCamisa).CurrentValues.SetValues(dto.Camisa);
                        _context.DetalleCamisas.Add(nuevaCamisa);
                    }
                }
                else if (ordenExistente.DetalleCamisa != null) _context.DetalleCamisas.Remove(ordenExistente.DetalleCamisa);
                // 7. Detalle Zapato
                if (dto.Zapato != null)
                {
                    if (ordenExistente.DetalleZapato != null)
                    {
                        _context.Entry(ordenExistente.DetalleZapato).CurrentValues.SetValues(dto.Zapato);
                        _context.Entry(ordenExistente.DetalleZapato).Property(x => x.IdDetalleZapato).IsModified = false;
                        _context.Entry(ordenExistente.DetalleZapato).Property(x => x.IdOrden).IsModified = false;
                    }
                    else
                    {
                        var nuevoZapato = new DetalleZapato { IdOrden = id };
                        _context.Entry(nuevoZapato).CurrentValues.SetValues(dto.Zapato);
                        _context.DetalleZapatos.Add(nuevoZapato);
                    }
                }
                else if (ordenExistente.DetalleZapato != null) _context.DetalleZapatos.Remove(ordenExistente.DetalleZapato);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Orden integral actualizada con éxito", id = ordenExistente.IdOrden });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error al actualizar la orden: {ex.Message} {ex.InnerException?.Message}");
            }
        }

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
                .Include(o => o.DetalleZapato)
                .Where(o => o.IdSucursal == idSucursal)
                .OrderByDescending(o => o.FechaCreacion)
                .ToListAsync();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden == null) return NotFound();

            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Expediente eliminado correctamente." });
        }
    }
}