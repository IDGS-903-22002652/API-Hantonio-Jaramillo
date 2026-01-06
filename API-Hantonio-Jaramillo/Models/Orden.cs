using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("ORDEN")]
public class Orden
{
    [Key]
    [Column("id_orden")]
    public int IdOrden { get; set; }

    [Column("id_cliente")]
    public int? IdCliente { get; set; }

    [Column("id_usuario_creador")]
    public int? IdUsuarioCreador { get; set; }

    [Column("id_sucursal")]
    public int? IdSucursal { get; set; }

    [Column("id_tipo_traje")]
    public int? IdTipoTraje { get; set; }

    [Column("id_estatus")]
    public int? IdEstatus { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("fecha_cita_medidas")]
    public DateTime? FechaCitaMedidas { get; set; }

    [Column("fecha_evento_entrega")]
    public DateTime? FechaEventoEntrega { get; set; }

    [Column("costo_total")]
    public decimal? CostoTotal { get; set; }

    [Column("monto_abonado")]
    public decimal? MontoAbonado { get; set; }

    [Column("incluye_camisa")]
    public bool IncluyeCamisa { get; set; } = false;

    // Navegación
    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }

    [ForeignKey("IdUsuarioCreador")]
    public Usuario? UsuarioCreador { get; set; }

    [ForeignKey("IdSucursal")]
    public Sucursal? Sucursal { get; set; }

    [ForeignKey("IdTipoTraje")]
    public CatTipoTraje? TipoTraje { get; set; }

    [ForeignKey("IdEstatus")]
    public CatEstatus? Estatus { get; set; }

    public MedidasOrden? MedidasOrden { get; set; }
    public DetalleSaco? DetalleSaco { get; set; }
    public DetalleCamisa? DetalleCamisa { get; set; }
}