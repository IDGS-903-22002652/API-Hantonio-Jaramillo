using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("DETALLE_CAMISA")]
public class DetalleCamisa
{
    [Key]
    [Column("id_detalle_camisa")]
    public int IdDetalleCamisa { get; set; }

    [Column("id_orden")]
    public int? IdOrden { get; set; }

    [Column("codigo_tela")]
    [MaxLength(50)]
    public string? CodigoTela { get; set; }

    [Column("estilo_cuello")]
    [MaxLength(50)]
    public string? EstiloCuello { get; set; }

    [Column("estilo_puno")]
    [MaxLength(50)]
    public string? EstiloPuno { get; set; }

    [Column("iniciales")]
    [MaxLength(10)]
    public string? Iniciales { get; set; }

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    // Navegación
    [ForeignKey("IdOrden")]
    public Orden? Orden { get; set; }
}