using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("DETALLE_SACO")]
public class DetalleSaco
{
    [Key]
    [Column("id_detalle_saco")]
    public int IdDetalleSaco { get; set; }

    [Column("id_orden")]
    public int? IdOrden { get; set; }

    [Column("codigo_tela")]
    [MaxLength(50)]
    public string? CodigoTela { get; set; }

    [Column("estilo_solapa")]
    [MaxLength(50)]
    public string? EstiloSolapa { get; set; }

    [Column("estilo_bolsillo")]
    [MaxLength(50)]
    public string? EstiloBolsillo { get; set; }

    [Column("codigo_boton")]
    [MaxLength(50)]
    public string? CodigoBoton { get; set; }

    [Column("monograma")]
    [MaxLength(100)]
    public string? Monograma { get; set; }

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    // Navegación
    [ForeignKey("IdOrden")]
    public Orden? Orden { get; set; }
}