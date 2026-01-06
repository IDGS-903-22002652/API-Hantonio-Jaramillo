using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hantonio_Jaramillo.Models;

[Table("MEDIDAS_ORDEN")]
public class MedidasOrden
{
    [Key]
    [Column("id_medida")]
    public int IdMedida { get; set; }

    [Column("id_orden")]
    public int IdOrden { get; set; }

    // Saco
    [Column("s_hombros")]
    public decimal? SHombros { get; set; }

    [Column("s_pecho")]
    public decimal? SPecho { get; set; }

    [Column("s_estomago")]
    public decimal? SEstomago { get; set; }

    [Column("s_largo_frente")]
    public decimal? SLargoFrente { get; set; }

    // Pantalón
    [Column("p_cintura")]
    public decimal? PCintura { get; set; }

    [Column("p_cadera")]
    public decimal? PCadera { get; set; }

    [Column("p_tiro")]
    public decimal? PTiro { get; set; }

    [Column("p_largo")]
    public decimal? PLargo { get; set; }

    // Camisa
    [Column("c_cuello")]
    public decimal? CCuello { get; set; }

    [Column("c_manga")]
    public decimal? CManga { get; set; }

    [Column("observaciones_medidas")]
    public string? ObservacionesMedidas { get; set; }

    // Navegación
    [ForeignKey("IdOrden")]
    public Orden? Orden { get; set; }
}